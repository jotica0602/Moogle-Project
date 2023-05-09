namespace MoogleEngine;

    class Matrix{
    static double Det(int [,] matrix)
    {
        int n = matrix.GetLength(0);
        double det = 0;

        if (n == 1){
            det = matrix[0, 0];
        }
        else if (n == 2){
            det = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        else{
            for (int j = 0; j < n; j++){

                int [,] minor = new int[n - 1, n - 1];
                for (int i = 1; i < n; i++){
                    for (int k = 0; k < n; k++){
                        if (k < j){
                            minor[i - 1, k] = matrix[i, k];
                        }
                        else if (k > j){
                            minor[i - 1, k - 1] = matrix[i, k];
                        }
                    }
                }
                det += Math.Pow(-1, j) * matrix[0, j] * Det(minor);
            }
        }

        return det;
    }

    static int [,] Transpose (int[,]matrix){
        int [,] transposed = new int [matrix.GetLength(1),matrix.GetLength(0)];
        for(int i =0;i<transposed.GetLength(0);i++){
            for(int j=0;j<transposed.GetLength(1);j++){
                transposed[i,j]=matrix[j,i];
            }
        }
        return transposed;
    }

    static int[,] Sum(int[,] matrixA, int[,] matrixB){
        int[,] matrixC = new int[matrixA.GetLength(0), matrixA.GetLength(1)];
        if (matrixA.Length != matrixB.Length) throw new Exception("Invalid Dimensions");
        for (int i = 0; i < matrixC.GetLength(0); i++){
            for (int j = 0; j < matrixC.GetLength(1); j++){
                matrixC[i, j] = matrixA[i, j] + matrixB[i, j];
            }
        }
        return matrixC;
    }

    static int[,] Diff(int[,] matrixA, int[,] matrixB){
        int[,] matrixC = new int[matrixA.GetLength(0), matrixA.GetLength(1)];
        if (matrixA.Length != matrixB.Length) throw new Exception("Invalid Dimensions");
        for (int i = 0; i < matrixC.GetLength(0); i++){
            for (int j = 0; j < matrixC.GetLength(1); j++){
                matrixC[i, j] = matrixA[i, j] - matrixB[i, j];
            }
        }
        return matrixC;
    }


    static int[,] Mult(int[,] matrixA, int[,] matrixB){
        int [,] matrixC = new int [matrixA.GetLength(0),matrixB.GetLength(1)];
        if(matrixA.GetLength(1)!=matrixB.GetLength(0)) throw new Exception("Invalid Dimensions");
        for(int i =0;i<matrixC.GetLength(0);i++){
            for(int j=0;j<matrixC.GetLength(1);j++){
                for(int k=0;k<matrixA.GetLength(1);k++){
                    matrixC[i,j] += matrixA[i,k]*matrixB[k,j];
                }
            }
        }
        return matrixC;
    }
    static int [,] ScalarMult(int[,] matrix, int scalar){
        int [,] matrixB=matrix;
        for(int i =0;i<matrixB.GetLength(0);i++){
            for(int j=0;j<matrixB.GetLength(1);j++){
                matrixB[i,j]*=scalar;
            }
        }
        
        return matrixB;
    }

    void PrintMatrix(int[,] matrixC)
    {
        for (int i = 0; i < matrixC.GetLength(0); i++){
            for (int j = 0; j < matrixC.GetLength(1); j++)
            {
                Console.Write($"{matrixC[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}
