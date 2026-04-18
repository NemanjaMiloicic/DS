#include"mpi.h"
#include<stdio.h>
#include<stdlib.h>
#include"math.h"
#define N 9
#define MASTER 0

void printFile(char filePath[] , int size)
{
	FILE* f = fopen(filePath, "rb");
	int total = size*N;
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
	int rank, M;
	MPI_File file1, file2;
	char file1Path[] = "file1.dat";
	char file2Path[] = "file2.dat";
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);
	MPI_Comm_size(MPI_COMM_WORLD, &M);
	int* buf = (int*)malloc(N * sizeof(int));
	for (int i = 0; i < N; i++)
		buf[i] = rank;
	//1.
	MPI_Offset offset = (MPI_Offset)(M - rank - 1) * N * sizeof(int);
	MPI_File_open(MPI_COMM_WORLD, file1Path,
		MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &file1);
	MPI_File_seek(file1, offset, MPI_SEEK_SET);
	MPI_File_write(file1, buf, N, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&file1);
	free(buf);
	MPI_Barrier(MPI_COMM_WORLD);
	/*if (rank == MASTER)
		printFile(file1Path, M);*/
	//2.
	MPI_File_open(MPI_COMM_WORLD, file1Path,
		MPI_MODE_RDONLY, MPI_INFO_NULL, &file1);
	int* readBuf = (int*)malloc(N * sizeof(int));
	MPI_File_read_at(file1, offset, readBuf, N, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&file1);
	/*for (int i = 0; i < N; i++)
		printf("%d", readBuf[i]);*/
	MPI_Barrier(MPI_COMM_WORLD);
	//3.
	//odredjivanje psizes
	int bestI = 1, bestJ = M; // tako da budu sto "kvadratniji" , nalazenjem faktora
	int minDiff = M; // nije jasno u zadatku da li je uvek 2x3 ili treba ovako... vrlo moguce da je uvek 2x3
	if (rank == MASTER) {
		for (int i = 1; i <= sqrt(M); i++)
		{
			if (M % i == 0) {
				int j = M / i;
				if (abs(i - j) < minDiff) 
				{
					minDiff = abs(i - j);
					bestI = i;
					bestJ = j;
				}
			}
		}
	}
	int gsizes[2];
	int distribs[2];
	int dargs[2];
	int psizes[2];
	gsizes[0] = M;
	gsizes[1] = N;
	distribs[0] = MPI_DISTRIBUTE_BLOCK;
	distribs[1] = MPI_DISTRIBUTE_BLOCK;
	dargs[0] = MPI_DISTRIBUTE_DFLT_DARG;
	dargs[1] = MPI_DISTRIBUTE_DFLT_DARG;
	MPI_Bcast(&bestI, 1, MPI_INT, MASTER, MPI_COMM_WORLD);
	MPI_Bcast(&bestJ, 1, MPI_INT, MASTER, MPI_COMM_WORLD);
	MPI_Barrier(MPI_COMM_WORLD);
	psizes[0] = bestI; //ako ne treba da se racuna onda samo upisemo ovde 2
	psizes[1] = bestJ; // ovde 3
	MPI_Datatype darray;
	MPI_Type_create_darray(M, rank, 2, gsizes, distribs, dargs, psizes, MPI_ORDER_C, MPI_INT, &darray);
	MPI_Type_commit(&darray);
	MPI_File_open(MPI_COMM_WORLD, file2Path,
		MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &file2);
	MPI_File_set_view(file2, 0, MPI_INT, darray, "native", MPI_INFO_NULL);
	MPI_File_write_all(file2, readBuf, N, MPI_INT, MPI_STATUS_IGNORE);
	free(readBuf);
	MPI_File_close(&file2);
	MPI_Barrier(MPI_COMM_WORLD);
	if (rank == MASTER)
		printFile(file2Path, M);
	MPI_Finalize();
}