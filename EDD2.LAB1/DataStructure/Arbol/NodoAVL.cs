using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructure.Arbol
{
    public class NodoAVL<T>
    {
        public T Value { get; set; }

        //Nodo Uno
        public NodoAVL<T> One { get; set; }

        //Nodo Dos
        public NodoAVL<T> Two { get; set; }

        public int ItsBalance { get; set; }

        //Constructor
        public NodoAVL(T Value)
        {
            this.Value = Value;
            One = null;
            Two = null;
        }
    }
}
