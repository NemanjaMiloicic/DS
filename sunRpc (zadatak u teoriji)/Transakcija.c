// sunrpc.x
struct transakcija 
{
  int id;
  float iznos;
};

struct rezultat
{
  float originalanIznos;
  float evro;
  float funta;
};

program TRANSAKCIJA
{
  version TRANSAKCIJA_VERSION
  {
     rezultat PRERACUNAJ(transakcija) = 1;
  }=1;
}=0x29999999;

//client.c
#include"sunrpc.h"
#include<rpc/rpc.h>
#include<stdio.h>
#include<stdlib.h>
//
int main (int argc, char** argv)
{
CLIENT* cln;
char* server = argv[1];
cln = clnt_create(server, TRANSAKCIJA, TRANSAKCIJA_VERSION, "udp");
transakcija t;
t.id = atoi(argv[2]);
t.iznos = atof(argv[3]);
rezultat* rez;
rez = preracunaj_1(&t, cln);
printf("originalan iznos:%f\n", rez->originalanIznos);
printf("evro:%f\n", rez->evro);
printf("funta:%f\n", rez->funta);
clnt_destroy(cln);
exit(0);
}

//server.c
#include"sunrpc.h"
#include<rpc/rpc.h>
//

rezultat* preracunaj_1_svc(transakcija* t, struct svc_req *rqstp)
{
    static rezultat r;

    r.originalanIznos = t->iznos;
    r.evro = t->iznos * 0.0085;   // primer kursa
    r.funta = t->iznos * 0.0073;  // primer kursa

    return &r;
}
