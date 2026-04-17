#include"mpi.h"
#include<stdio.h>
#include<stdlib.h>
#define FILESIZE 10*1024*1024
#define MASTER 0
void printFile(char filePath[]) //funkcija za validaciju resenja
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
	MPI_Offset offset = (MPI_Offset)rank*localInts * sizeof(int); // istu kolicinu podataka citaju redom
	int* buf = (int*)malloc(localInts * sizeof(int));
	for (int i = 0; i < localInts; i++)
		buf[i] = rank;
	MPI_File_open(MPI_COMM_WORLD, file1Path,
		MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &file1);
	MPI_File_seek(file1, offset, MPI_SEEK_SET);
	MPI_File_write(file1, buf, localInts, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&file1);
	free(buf);
	//dovde je bilo kreiranje fajla
	//odavde krece zadatak januar 22
	int* readBuf = (int*)malloc(localInts * sizeof(int));
	MPI_File_open(MPI_COMM_WORLD, file1Path,
		MPI_MODE_RDONLY, MPI_INFO_NULL, &file1);
	MPI_File_seek(file1, offset, MPI_SEEK_SET);
	MPI_File_read(file1, readBuf, localInts,MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&file1);
	MPI_Barrier(MPI_COMM_WORLD);
	MPI_Datatype vector;
	MPI_Type_vector(2, localInts / 2, size*localInts/2, MPI_INT, &vector); // 2 bloka, duzine polovine podataka, na udaljenosti za N polovina podataka
	MPI_Type_commit(&vector);
	MPI_File_open(MPI_COMM_WORLD, file2Path,
		MPI_MODE_CREATE|MPI_MODE_WRONLY, MPI_INFO_NULL, &file2);
	MPI_File_set_view(file2, rank*sizeof(int)*localInts/2, MPI_INT, vector, "native", MPI_INFO_NULL); // view za p1 pomeren za jednu polovinu
	MPI_File_write_all(file2, readBuf, localInts, MPI_INT, MPI_STATUS_IGNORE); // za p2 dve polovine , p3 3 polovine
	MPI_Barrier(MPI_COMM_WORLD);
	MPI_File_close(&file2);
	if (rank == MASTER)
		printFile(file2Path);
	free(readBuf);
	MPI_Finalize();
}