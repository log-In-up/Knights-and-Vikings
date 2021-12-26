using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class KdTree<T> : IEnumerable<T>, IEnumerable where T : Component
    {
        protected int _count;
        protected bool _just2D;
        protected float _LastUpdate = -1f;
        protected KdNode _root;
        protected KdNode _last;
        protected KdNode[] _open;

        public int Count { get { return _count; } }
        public bool IsReadOnly { get { return false; } }
        public float AverageSearchLength { protected set; get; }
        public float AverageSearchDeep { protected set; get; }

        /// <summary>
        /// Create a tree.
        /// </summary>
        /// <param name="just2D">Just use x/z.</param>
        public KdTree(bool just2D = false)
        {
            _just2D = just2D;
        }

        public T this[int key]
        {
            get
            {
                if (key >= _count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                KdNode current = _root;

                for (int i = 0; i < key; i++)
                {
                    current = current.next;
                }

                return current.component;
            }
        }

        /// <summary>
        /// Add item.
        /// </summary>
        /// <param name="item">Item.</param>
        public void Add(T item)
        {
            AddNode(new KdNode() { component = item });
        }

        /// <summary>
        /// Batch add items.
        /// </summary>
        /// <param name="items">Items</param>
        public void AddAll(List<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Find all objects that matches the given predicate.
        /// </summary>
        /// <param name="match">Lamda expression.</param>
        public KdTree<T> FindAll(Predicate<T> match)
        {
            KdTree<T> list = new(_just2D);

            foreach (T node in this)
            {
                if (match(node))
                {
                    list.Add(node);
                }
            }

            return list;
        }

        /// <summary>
        /// Find first object that matches the given predicate.
        /// </summary>
        /// <param name="match">Lamda expression.</param>
        public T Find(Predicate<T> match)
        {
            KdNode current = _root;
            while (current != null)
            {
                if (match(current.component))
                {
                    return current.component;
                }

                current = current.next;
            }
            return null;
        }

        /// <summary>
        /// Remove at position i (position in list or loop).
        /// </summary>
        public void RemoveAt(int i)
        {
            List<KdNode> list = new(GetNodes());
            list.RemoveAt(i);
            Clear();
            foreach (KdNode node in list)
            {
                node.oldRef = null;
                node.next = null;
            }
            foreach (KdNode node in list)
            {
                AddNode(node);
            }
        }

        /// <summary>
        /// Remove all objects that matches the given predicate.
        /// </summary>
        /// <param name="match">Lamda expression.</param>
        public void RemoveAll(Predicate<T> match)
        {
            List<KdNode> list = new(GetNodes());
            list.RemoveAll(n => match(n.component));
            Clear();
            foreach (KdNode node in list)
            {
                node.oldRef = null;
                node.next = null;
            }
            foreach (KdNode node in list)
            {
                AddNode(node);
            }
        }

        /// <summary>
        /// Count all objects that matches the given predicate.
        /// </summary>
        /// <param name="match">Lamda expression.</param>
        /// <returns>Matching object count.</returns>
        public int CountAll(Predicate<T> match)
        {
            int count = 0;
            foreach (T node in this)
            {
                if (match(node))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Clear tree.
        /// </summary>
        public void Clear()
        {
            //rest for the garbage collection
            _root = null;
            _last = null;
            _count = 0;
        }

        /// <summary>
        /// Update positions (if objects moved).
        /// </summary>
        /// <param name="rate">Updates per second.</param>
        public void UpdatePositions(float rate)
        {
            if (Time.timeSinceLevelLoad - _LastUpdate < 1f / rate)
            {
                return;
            }

            _LastUpdate = Time.timeSinceLevelLoad;

            UpdatePositions();
        }

        /// <summary>
        /// Update positions (if objects moved).
        /// </summary>
        public void UpdatePositions()
        {
            //save old traverse
            KdNode current = _root;
            while (current != null)
            {
                current.oldRef = current.next;
                current = current.next;
            }

            //save root
            current = _root;

            //reset values
            Clear();

            //readd
            while (current != null)
            {
                AddNode(current);
                current = current.oldRef;
            }
        }

        /// <summary>
        /// Method to enable foreach-loops.
        /// </summary>
        /// <returns>Enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            KdNode current = _root;
            while (current != null)
            {
                yield return current.component;
                current = current.next;
            }
        }

        /// <summary>
        /// Convert to list.
        /// </summary>
        /// <returns>List.</returns>
        public List<T> ToList()
        {
            List<T> list = new();
            foreach (T node in this)
            {
                list.Add(node);
            }

            return list;
        }

        /// <summary>
        /// Method to enable foreach-loops.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected float Vector3Distance(Vector3 a, Vector3 b)
        {
            if (_just2D)
            {
                return (a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z);
            }
            else
            {
                return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
            }
        }

        protected float GetSplitedValue(int level, Vector3 position)
        {
            if (_just2D)
            {
                return (level % 2 == 0) ? position.x : position.z;
            }
            else
            {
                return (level % 3 == 0) ? position.x : (level % 3 == 1) ? position.y : position.z;
            }
        }

        private void AddNode(KdNode newNode)
        {
            _count++;
            newNode.left = null;
            newNode.right = null;
            newNode.level = 0;
            KdNode parent = FindParentNode(newNode.component.transform.position);

            //set last
            if (_last != null)
            {
                _last.next = newNode;
            }

            _last = newNode;

            //set root
            if (parent == null)
            {
                _root = newNode;
                return;
            }

            float splitParent = GetSplitedValue(parent);
            float splitNew = GetSplitedValue(parent.level, newNode.component.transform.position);

            newNode.level = parent.level + 1;

            if (splitNew < splitParent)
            {
                parent.left = newNode; //go left
            }
            else
            {
                parent.right = newNode; //go right
            }
        }

        private KdNode FindParentNode(Vector3 position)
        {
            //travers from root to bottom and check every node
            KdNode current = _root;
            KdNode parent = _root;
            while (current != null)
            {
                float splitCurrent = GetSplitedValue(current);
                float splitSearch = GetSplitedValue(current.level, position);

                parent = current;
                if (splitSearch < splitCurrent)
                {
                    current = current.left; //go left
                }
                else
                {
                    current = current.right; //go right
                }
            }
            return parent;
        }

        /// <summary>
        /// Find closest object to given position.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <returns>Closest object.</returns>
        public T FindClosest(Vector3 position)
        {
            return FindClosestComponent(position);
        }

        /// <summary>
        /// Find close objects to given position.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <returns>Close object.</returns>
        public IEnumerable<T> FindClose(Vector3 position)
        {
            List<T> output = new();
            FindClosestComponent(position, output);
            return output;
        }

        protected T FindClosestComponent(Vector3 position, List<T> traversed = null)
        {
            if (_root == null)
            {
                return null;
            }

            float nearestDist = float.MaxValue;
            KdNode nearest = null;

            if (_open == null || _open.Length < Count)
            {
                _open = new KdNode[Count];
            }

            for (int i = 0; i < _open.Length; i++)
            {
                _open[i] = null;
            }

            int openAdd = 0;
            int openCur = 0;

            if (_root != null)
            {
                _open[openAdd++] = _root;
            }

            while (openCur < _open.Length && _open[openCur] != null)
            {
                KdNode current = _open[openCur++];
                if (traversed != null)
                {
                    traversed.Add(current.component);
                }

                float nodeDist = Vector3Distance(position, current.component.transform.position);
                if (nodeDist < nearestDist)
                {
                    nearestDist = nodeDist;
                    nearest = current;
                }

                float splitCurrent = GetSplitedValue(current);
                float splitSearch = GetSplitedValue(current.level, position);

                if (splitSearch < splitCurrent)
                {
                    if (current.left != null)
                    {
                        _open[openAdd++] = current.left; //go left
                    }

                    if (Mathf.Abs(splitCurrent - splitSearch) * Mathf.Abs(splitCurrent - splitSearch) < nearestDist && current.right != null)
                    {
                        _open[openAdd++] = current.right; //go right
                    }
                }
                else
                {
                    if (current.right != null)
                    {
                        _open[openAdd++] = current.right; //go right
                    }

                    if (Mathf.Abs(splitCurrent - splitSearch) * Mathf.Abs(splitCurrent - splitSearch) < nearestDist && current.left != null)
                    {
                        _open[openAdd++] = current.left; //go left
                    }
                }
            }

            AverageSearchLength = (99f * AverageSearchLength + openCur) / 100f;
            AverageSearchDeep = (99f * AverageSearchDeep + nearest.level) / 100f;

            return nearest.component;
        }

        private float GetSplitedValue(KdNode node)
        {
            return GetSplitedValue(node.level, node.component.transform.position);
        }

        private IEnumerable<KdNode> GetNodes()
        {
            KdNode current = _root;
            while (current != null)
            {
                yield return current;
                current = current.next;
            }
        }

        protected class KdNode
        {
            internal T component;
            internal int level;
            internal KdNode left;
            internal KdNode right;
            internal KdNode next;
            internal KdNode oldRef;
        }
    }
}