#include"mpi.h"
#include<stdio.h>
#include<stdlib.h>
#define N 4
#define MASTER 0

void printFile(char filePath[], int size)
{
	FILE* f = fopen(filePath, "rb");
	int total = size * N;
	char* buf = (char*)malloc(total * sizeof(char));

	fread(buf, sizeof(char), total, f);
	fclose(f);

	for (int i = 0; i < total; i++)
		printf("%c", buf[i]);
	printf("\n");

	free(buf);
}

int main(int argc, char**argv)
{
	int rank, size;
	MPI_Init(&argc, &argv);
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_File file1, file2;
	MPI_Offset offset = (MPI_Offset)rank*N*sizeof(char);
	char file1Path[] = "prva.dat";
	char file2Path[] = "druga.dat";
	char text[N] = "ni";
	text[N - 2] = rank + '0';
	//a
	MPI_File_open(MPI_COMM_WORLD, file1Path,
		MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &file1);
	MPI_File_seek(file1, offset, MPI_SEEK_SET);
	MPI_File_write(file1, text, N, MPI_CHAR, MPI_STATUS_IGNORE);
	MPI_File_close(&file1);
	MPI_Barrier(MPI_COMM_WORLD);
	/*if (rank == MASTER)
		printFile(file1Path, size);*/
	char readText[N];
	MPI_File_open(MPI_COMM_WORLD, file1Path,
		MPI_MODE_RDONLY, MPI_INFO_NULL, &file1);
	MPI_File_read_at(file1, offset, readText, N, MPI_CHAR, MPI_STATUS_IGNORE);
	MPI_File_close(&file1);
	MPI_Barrier(MPI_COMM_WORLD);
	//for (int i = 0; i < N; i++)
	//{
	//	printf("%c ", readText[i]);
	//}
	//c)
	MPI_Datatype vector;
	MPI_Type_vector(N, 2 * N / size, 2 * N, MPI_CHAR, &vector);
	MPI_Type_commit(&vector);
	MPI_File_open(MPI_COMM_WORLD, file2Path,
		MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &file2);
	MPI_File_set_view(file2, 2 * N / size * rank, MPI_CHAR, vector, "native", MPI_INFO_NULL);
	MPI_File_write_all(file2, readText, N, MPI_CHAR, MPI_STATUS_IGNORE);
	MPI_Barrier(MPI_COMM_WORLD);
	MPI_File_close(&file2);
	if (rank == MASTER)
		printFile(file2Path, size);
	MPI_Finalize();
}