using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructure.Arbol
{
    public class ArbolAVL<T> : IEnumerable<T>
    {
        public NodoAVL<T> Root;
        private Func<T, T, int> compare;
        public ArbolAVL(Func<T, T, int> comparer)
        {
            Root = null;
            this.compare = comparer;
        }
        public virtual bool IsEmpty
        {
            get { return this.Root == null; }
        }

        //Metod to add in AVL Tree
        //One left
        public virtual void Add(T item)
        {
            bool Flag = false;
            this.Root = AddToAVL(this.Root!, item, ref Flag);
        }
        public virtual NodoAVL<T> AddToAVL(NodoAVL<T> Root, T item, ref bool Flag)
        {
            NodoAVL<T> NodeToADD;
            if (Root == null)
            {
                Root = new NodoAVL<T>(item);
                Flag = true;
            }
            else
            {
                if (compare(item, Root.Value) < 0 )//Enters if the value is less than the value on the actual node.
                {
                    Root.One = AddToAVL(Root.One!, item, ref Flag);
                    if (Flag)
                    {
                        switch (Root.ItsBalance)
                        {
                            case -1:
                                Root.ItsBalance = 0;
                                Flag = false;
                                break;
                            case 0:
                                Root.ItsBalance = 1;
                                break;
                            case 1:
                                NodeToADD = Root.One;
                                if (NodeToADD.ItsBalance == 1)
                                {
                                    Root = SimpleRightRotation(Root, NodeToADD); //Simple Right Rotation
                                }
                                else
                                {
                                    Root = DoubleRightRotation(Root, NodeToADD); //Double Right Rotation
                                }
                                Flag = false;
                                break;
                        }
                    }
                }
                else //Enters if the value is greather than the value on the actual node.
                {
                    if (compare(item, Root.Value) > 0) //Verification of the value beeing greather.
                    {
                        Root.Two = AddToAVL(Root.Two!, item, ref Flag);
                        if (Flag)
                        {
                            switch (Root.ItsBalance)
                            {
                                case -1:
                                    NodeToADD = Root.Two;
                                    if (NodeToADD.ItsBalance == -1)
                                    {
                                        Root = SimpleLeftRotation(Root, NodeToADD); //Simple Left Rotation

                                    }
                                    else
                                    {
                                        Root = DoubleLeftRotation(Root, NodeToADD); //Double Left Rotation
                                    }
                                    Flag = false;
                                    break;
                                case 0:
                                    Root.ItsBalance = -1;
                                    break;
                                case 1:
                                    Root.ItsBalance = 0;
                                    Flag = false;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Root.Value = item;
                    }
                }
            }
            return Root;
        }

        public virtual NodoAVL<T> SimpleRightRotation(NodoAVL<T> Node, NodoAVL<T> Node1) //Simple Right Rotation
        {
            Node.One = Node1.Two;
            Node1.Two = Node;
            if (Node1.ItsBalance == 1)
            {
                Node.ItsBalance = 0;
                Node1.ItsBalance = 0;
            }
            else
            {
                Node.ItsBalance = 1;
                Node1.ItsBalance = -1;
            }
            return Node1;
        }

        public virtual NodoAVL<T> DoubleRightRotation(NodoAVL<T> Node, NodoAVL<T> Node1) //Double Right Rotation
        {
            NodoAVL<T> RNode = Node1.Two;
            Node.One = RNode!.Two;
            RNode.Two = Node;
            Node1.Two = RNode.One;
            RNode.One = Node1;
            Node1.ItsBalance = (RNode.ItsBalance == -1) ? 1 : 0;
            Node.ItsBalance = (RNode.ItsBalance == 1) ? -1 : 0;
            RNode.ItsBalance = 0;
            return RNode;
        }
        public virtual NodoAVL<T> SimpleLeftRotation(NodoAVL<T> Node, NodoAVL<T> Node1) //Simple Left Rotation
        {
            Node.Two = Node1.One;
            Node1.One = Node;
            if (Node1.ItsBalance == -1)
            {
                Node.ItsBalance = 0;
                Node1.ItsBalance = 0;
            }
            else
            {
                Node.ItsBalance = -1;
                Node1.ItsBalance = -1;
            }
            return Node1;
        }
        public virtual NodoAVL<T> DoubleLeftRotation(NodoAVL<T> Node, NodoAVL<T> Node1) //Double Left Rotation
        {
            NodoAVL<T> RNode = Node1.One;
            Node.Two = RNode!.One;
            RNode.One = Node;
            Node1.One = RNode.Two;
            RNode.Two = Node1;
            Node.ItsBalance = (RNode.ItsBalance == -1) ? 1 : 0;
            Node1.ItsBalance = (RNode.ItsBalance == 1) ? -1 : 0;
            RNode.ItsBalance = 0;
            return RNode;
        }

        public void Remove(NodoAVL<T> NodeRootToSearch, NodoAVL<T> NodeSearch)
        {
            NodoAVL<T> current = NodeRootToSearch;
            NodoAVL<T> parent = NodeRootToSearch;
            bool IsChildLeft = false;


            if (current == null)
            {
                return;
            }

            while (current != null && compare(current.Value, NodeSearch.Value) != 0)
            {
                parent = current;

                if (compare(NodeSearch.Value, current.Value)  < 0)
                {
                    current = current.One;
                    IsChildLeft = true;
                }
                else
                {
                    current = current.Two;
                    IsChildLeft = false;
                }
            }

            if (current == null)
            {
                return;
            }

            if (current.Two == null && current.One == null)
            {
                if (current == Root)
                {
                    Root = null;
                }
                else
                {
                    if (IsChildLeft)
                    {
                        parent.One = null;
                    }
                    else
                    {
                        parent.Two = null;
                    }
                }
            }
            else if (current.Two == null)
            {
                if (current == Root)
                {
                    Root = current.One;
                }
                else
                {
                    if (IsChildLeft)
                    {
                        parent.One = current.One;
                    }
                    else
                    {
                        parent.Two = current.One;
                    }
                }
            }
            else if (current.One == null)
            {
                if (current == Root)
                {
                    Root = current.Two;
                }
                else
                {
                    if (IsChildLeft)
                    {
                        parent.One = current.Two;
                    }
                    else
                    {
                        parent.Two = current.Two;
                    }
                }
            }
            else
            {
                if (current == Root)
                {
                    NodoAVL<T> successor = RightestfromLeft(current);
                    Root = successor;
                }
                else if (IsChildLeft)
                {
                    NodoAVL<T> successor = MostLeftFromRight(current);
                    parent.One = successor;
                }
                else
                {
                    NodoAVL<T> successor = RightestfromLeft(current);
                    parent.Two = successor;
                }
            }

            int b = getBalance(parent, CompareObj);
            if (b > 1 && getBalance(parent.One, CompareObj) >= 0)
            {
                parent = SimpleRightRotation(parent, parent.One);
            }
            else if (b > 1 && getBalance(parent.One, CompareObj) < 0)
            {
                parent = DoubleRightRotation(parent, parent.One);
            }
            else if (b < 1 && getBalance(parent.Two, CompareObj) <= 0)
            {
                parent = SimpleLeftRotation(parent, parent.Two);
            }
            else
            {
                parent = DoubleLeftRotation(parent, parent.Two);
            }
        }

        private NodoAVL<T> RightestfromLeft(NodoAVL<T> Node)
        {
            NodoAVL<T> Temp = Node.One;
            NodoAVL<T> PreTemp = Node.One;
            NodoAVL<T> CurrentLeft = Node.One;
            NodoAVL<T> CurrentRight = Node.Two;
            bool NoSubTree = true;

            while (Temp!.Two != null)
            {
                PreTemp = Temp;
                Temp = Temp.Two;
                NoSubTree = false;
            }
            PreTemp.Two = null;
            if (NoSubTree)
            {
                Temp.One = CurrentLeft!.One;
            }
            else
            {
                Temp.One = CurrentLeft;
            }
            Temp.Two = CurrentRight;
            return Temp;
        }

        private NodoAVL<T> MostLeftFromRight(NodoAVL<T> Node)
        {
            NodoAVL<T> Temp = Node.Two;
            NodoAVL<T> PreTemp = Node.Two;
            NodoAVL<T> CurrentLeft = Node.One;
            NodoAVL<T> CurrentRight = Node.Two;
            bool NoSubTree = true;

            while (Temp!.One != null)
            {
                PreTemp = Temp;
                Temp = Temp.One;
                NoSubTree = false;
            }
            PreTemp.One = null;
            Temp.One = CurrentLeft;
            if (NoSubTree)
            {
                Temp.Two = CurrentRight!.Two;
            }
            else
            {
                Temp.Two = CurrentRight;
            }
            return Temp;
        }

        public int getHeight(NodoAVL<T> node, Delegate CompareObj)
        {
            NodoAVL<T> temp = Root;
            int cont = 0; //Contador
            if (node == null) //Se verifica que no sea nulo
            {
                return -1000;
            }
            else
            {
                while (temp != null && compare(node.Value, temp.Value) != 0)
                {
                    if (compare(node.Value, temp.Value) > 0)
                    {
                        temp = temp.Two;
                        cont++;
                    }
                    else
                    {
                        temp = temp.One;
                        cont++;
                    }
                }
                return cont;
            }

        }

        public int getBalance(NodoAVL<T> node, Delegate CompareObj)
        {
            if (node == null)
            {
                return 0;
            }
            else
            {
                return getHeight(node.One, CompareObj) - getHeight(node.Two, CompareObj);
            }
        }
        public void InOrderRoute(NodoAVL<T> Node, Queue<T> Items)
        {
            if (Node!.One != null)
            {
                InOrderRoute(Node.One, Items);
            }
            Items.Enqueue(Node.Value);
            if (Node!.Two != null)
            {
                InOrderRoute(Node.Two, Items);
            }
        }

        public NodoAVL<T> SearchElement(T ElementToSearch)
        {
            NodoAVL<T> Temp = Root;
            while (Temp != null && compare(ElementToSearch, Temp!.Value) != 0)
            {
                if (compare(ElementToSearch, Temp!.Value) > 0)
                {
                    Temp = Temp.Two;
                }
                else
                {
                    Temp = Temp.One;
                }
            }
            return Temp;
        
        }
        public IEnumerator<T> GetEnumerator()
        {
            Queue<T> Elements = new Queue<T>();
            InOrderRoute(Root, Elements);
            while (Elements.Count != 0)
            {
                yield return Elements.Dequeue();
            }
        }



        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
