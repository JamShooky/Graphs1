﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    class UnderictedGraph<T>
    {
        List<Vertex<T>> vertices;

        public List<Vertex<T>> dft;
        public List<Vertex<T>> bft;
        int count { get { return vertices.Count; } }
        public UnderictedGraph() : this(10) {}
        public UnderictedGraph(int size)
        {
            dft = new List<Vertex<T>>();
            bft = new List<Vertex<T>>();
            vertices = new List<Vertex<T>>();
        }

        public void AddVertex(Vertex<T> vertex)
        {
            if(!vertices.Contains(vertex))
            vertices.Add(vertex);
        }

        public bool RemoveVertex(Vertex<T> vertex)
        {
            if (!vertices.Contains(vertex)) return false;
            for (int i = 0; i < vertex.AdjacentList.Count; i++)
            {
                RemoveEdge(vertex, vertex.AdjacentList[i]);
                i--;
            }
            vertices.Remove(vertex);
            return true;

        }

        public void AddEdge(Vertex<T> a, Vertex<T> b)
        {
            a.addNeighbors(b);
            b.addNeighbors(a);
        }

        public bool RemoveEdge(Vertex<T> a, Vertex<T> b)
        {
            if (!(a.AdjacentList.Contains(b) && b.AdjacentList.Contains(a))) return false;
            a.removeNeighbors(b);
            b.removeNeighbors(a);
            return true;
        }

        public void DepthFirstTraversal(Vertex<T> node)
        {
            node.Visited = true;
            dft.Add(node);
            for (int i = 0; i < node.AdjacentList.Count; i++)
            { 
                if (node.AdjacentList[i].Visited == false)
                {
                    DepthFirstTraversal(node.AdjacentList[i]);
                }
            }
        }

        public void BreadthFirstTraversal(Vertex<T> node) // make it recursive next time -__-
        {
            for (int i = 0; i < vertices.Count; i++) { vertices[i].Visited = false; };
            bft.Clear(); 
            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            node.Visited = true;
            bft.Add(node);
            for(int i = 0; i < node.AdjacentList.Count; i++)
            {
                node.AdjacentList[i].Visited = true;
                queue.Enqueue(node.AdjacentList[i]);
            }
            while (queue.Count != 0)
            {
                List<Vertex<T>> temp = new List<Vertex<T>>();
                temp = queue.Peek().AdjacentList;
                bft.Add(queue.Peek());
                queue.Dequeue();
                for(int i = 0; i < temp.Count; i++)
                {
                    if(!temp[i].Visited)
                    {
                        temp[i].Visited = true;
                        queue.Enqueue(temp[i]);
                    }
                }
            }
            for (int i = 0; i < vertices.Count; i++) { vertices[i].Visited = false; };
        }

    }
}
