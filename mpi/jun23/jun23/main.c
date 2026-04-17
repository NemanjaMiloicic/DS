#include"mpi.h"
#include<stdlib.h>
#include<stdio.h>
#define K 6
#define MASTER 0
void printFile(char filePath[], int size)
{
	FILE* f = fopen(filePath, "rb");
	int total = size * K;
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
	int rank, size;
	MPI_File file1,file2;
	char file1Path[] = "file1.dat";
	char file2Path[] = "file2.dat";
	MPI_Init(&argc, &argv);
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_File_open(MPI_COMM_WORLD, file1Path, 
		MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &file1);
	//a)
	MPI_Offset offset = (MPI_Offset)rank * K * sizeof(int);
	MPI_File_seek(file1, offset, MPI_SEEK_SET);
	int* inputBuf = (int*)malloc(K * sizeof(int));
	for (int i = 0; i < K; i++)
	{
		inputBuf[i] = i + (K * rank);
	}
	MPI_File_write(file1, inputBuf, K, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&file1);
	MPI_Barrier(MPI_COMM_WORLD);
	/*if (rank == MASTER)
		printFile(file1Path, size);*/
	free(inputBuf);
	//b)
	MPI_File_open(MPI_COMM_WORLD, file1Path,
		MPI_MODE_RDONLY, MPI_INFO_NULL, &file1);
	int* readBuf = (int*)malloc(K * sizeof(int));
	MPI_File_read_at(file1, offset, readBuf, K, MPI_INT, MPI_STATUS_IGNORE);
	/*for (int i = 0; i < K; i++)
		printf("%d ", readBuf[i]);*/
	//c)
	MPI_Datatype vector;
	MPI_Barrier(MPI_COMM_WORLD);
	MPI_Type_vector(size, K / size, K, MPI_INT, &vector);
	MPI_Type_commit(&vector);
	MPI_File_open(MPI_COMM_WORLD, file2Path, MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &file2);
	MPI_File_set_view(file2, rank * K / size*sizeof(int), MPI_INT, vector, "native", MPI_INFO_NULL);
	MPI_File_write_all(file2, readBuf, size*K, MPI_INT, MPI_STATUS_IGNORE);
	if (rank == MASTER)
		printFile(file2Path, size);
	free(readBuf);
	MPI_File_close(&file2);
	MPI_Finalize();
}