import java.io.Serializable;

public class Podatak implements Serializable {
    public int n;
    public int m;
    public ProstCallback clb;

    public int getN(){
        return n;
    }
    public int getM(){
        return m;
    }
    public ProstCallback getClb(){
        return clb;
    }
    public void setN(int n){
        this.n = n;
    }
    public void setM(int m){
        this.m = m;
    }
    public void setClb(ProstCallback clb){
        this.clb = clb;
    }

}
