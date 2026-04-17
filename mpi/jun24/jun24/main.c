#include"mpi.h"
#include<stdlib.h>
#include<stdio.h>
#define N 105
#define MASTER 0

void printFile(char filePath[], int size)
{
		FILE* f = fopen(filePath, "rb");
		int total = size * N;
		int* buf = (int*)malloc(total * sizeof(int));

		fread(buf, sizeof(int), total, f);
		fclose(f);

		for (int i = 0; i < total; i++)
			printf("%d ", buf[i]);
		printf("\n");

		free(buf);
}

int main(int argc, char** argv) 
{
	int size, rank;
	MPI_File file1, file2;
	MPI_Offset offset;
	char file1Path[] = "output.dat";
	char file2Path[] = "outputC.dat";
	MPI_Init(&argc, &argv);
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	int* inputBuf = (int*)malloc(N*sizeof(int));
	offset = (MPI_Offset)(size - 1 - rank) * N * sizeof(int); //jer naopacke upisuju zbog toga ovakav offset
	for (int i = 0; i < N; i++)
		inputBuf[i] = rank;
	//a)
	MPI_File_open(MPI_COMM_WORLD, file1Path, MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &file1);
	MPI_File_seek(file1, offset, MPI_SEEK_SET);
	MPI_File_write(file1, inputBuf, N , MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&file1);
	free(inputBuf);
	MPI_Barrier(MPI_COMM_WORLD);
	/*if (rank == MASTER)
		printFile(file1Path, size);
	*/
	//b)
	int* outputBuf = (int*)malloc(N * sizeof(int));
	MPI_File_open(MPI_COMM_WORLD, file1Path, MPI_MODE_RDONLY, MPI_INFO_NULL, &file1);
	MPI_File_read_at(file1, offset, outputBuf, N, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&file1);
	//for (int i = 0; i < N; i++)
	//	printf("%d_%d ", outputBuf[i] , rank);//svaki proces treba da procita podatke koje je on upisao
	//printf("\n");
	MPI_Barrier(MPI_COMM_WORLD);
	//c)
	int blocks = 1;
	if (rank == MASTER) {
		int blockData = 1;
		while (blockData != N)
		{
			blocks++;
			blockData += blocks;
		}
	}
	MPI_Bcast(&blocks, 1, MPI_INT, MASTER, MPI_COMM_WORLD);
	int* blockLengths = (int*)malloc(blocks * sizeof(int));
	int* blockDisplacements = (int*)malloc(blocks * sizeof(int));
	int displacement = 0;
	for (int i = 0; i < blocks; i++)
	{
		blockLengths[i] = i + 1;
		blockDisplacements[i] = displacement;
		displacement += blockLengths[i] * size + rank;
	}
	MPI_Datatype indexed;
	MPI_Type_indexed(blocks, blockLengths, blockDisplacements, MPI_INT, &indexed);
	MPI_Type_commit(&indexed);
	MPI_File_open(MPI_COMM_WORLD, file2Path, MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &file2);
	MPI_File_set_view(file2, rank*sizeof(int), MPI_INT, indexed, "native", MPI_INFO_NULL);
	MPI_File_write_all(file2, outputBuf, N, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&file2);
	free(outputBuf);
	free(blockLengths);
	free(blockDisplacements);
	if(rank == MASTER)
		printFile(file2Path, size);
	MPI_Finalize();
}