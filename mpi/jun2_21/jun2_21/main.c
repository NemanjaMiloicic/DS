#include"mpi.h"
#include<stdio.h>
#include<stdlib.h>
#define FILESIZE 1*1024*1024 //1mb
#define MASTER 0
void printFile(char filePath[])
{
	FILE* f = fopen(filePath, "rb");
	int total = FILESIZE / sizeof(int);
	int* buf = (int*)malloc(total * sizeof(int));

	fread(buf, sizeof(int), total, f);
	fclose(f);

	for (int i = 0; i < total; i++)
		printf("%d", buf[i]);
	printf("\n");

	free(buf);
}
int main(int argc, char** argv)
{
	int rank, size;
	MPI_File file1, file2;
	char file1Path[] = "file1.dat";
	char file2Path[] = "file2.dat";
	MPI_Init(&argc, &argv);
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	int totalInts = FILESIZE / sizeof(int);
	int localInts = totalInts / size;
	MPI_Offset offset = (MPI_Offset)rank * localInts * sizeof(int);
	int* buffer = (int*)malloc(localInts * sizeof(int));
	for (int i = 0; i < localInts; i++)
		buffer[i] = rank;
	//generisanje fajla od 1mb velicine da se posle lepo raspodeli na 0.25mb
	MPI_File_open(MPI_COMM_WORLD, file1Path,
		MPI_MODE_CREATE | MPI_MODE_WRONLY,MPI_INFO_NULL, &file1);
	MPI_File_write_at(file1, offset, buffer, localInts, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&file1);
	free(buffer);
	MPI_Barrier(MPI_COMM_WORLD);
	/*if (rank == MASTER)
		printFile(file1Path);*/
	//e sad krecemo haha
	int* readBuf = (int*)malloc(localInts * sizeof(int));
	MPI_Datatype contiguous;
	MPI_Type_contiguous(localInts, MPI_INT, &contiguous);
	MPI_Type_commit(&contiguous);
	MPI_File_open(MPI_COMM_WORLD, file1Path,
		MPI_MODE_RDONLY, MPI_INFO_NULL, &file1);
	MPI_File_set_view(file1, offset, MPI_INT, contiguous, "native", MPI_INFO_NULL);
	MPI_File_read_all(file1, readBuf, localInts, MPI_INT, MPI_STATUS_IGNORE);
	MPI_Barrier(MPI_COMM_WORLD);
	MPI_File_close(&file1);
		/*for (int i = 0; i < localInts; i++)
			printf("%d ", readBuf[i]);*/
	int blocks;
	if(rank == MASTER)
	{
		int blockData = 1;
		blocks = 1;
		while (blockData != localInts) 
		{
			blocks++;
			blockData += blocks;
		}
		printf("%d ", blocks);
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
	MPI_File_set_view(file2, rank * sizeof(int), MPI_INT, indexed, "native", MPI_INFO_NULL);
	MPI_File_write_all_begin(file2, readBuf, localInts, MPI_INT);
	int sum = 0;
	for (int i = 0; i < 1000; i++) 
	{
		sum += i;
	}
	MPI_File_write_all_end(file2, readBuf, MPI_STATUS_IGNORE);
	MPI_File_close(&file2);
	free(blockLengths);
	free(blockDisplacements);
	free(readBuf);
	if (rank == MASTER)
		printFile(file2Path);
	MPI_Finalize();

}
