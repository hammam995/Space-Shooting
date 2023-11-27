using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public interface IPool<T> where T : Component
    {
        public T Request(Vector3 position);
        public void Return(T obj);
    }
    public class Pool<T> : IPool<T> where T : Component
    {
        private Stack<T> objects = new();
        private T obj;
        public Pool(T instance)
        {
            obj = instance;
        }


        public T Request(Vector3 position)
        {
            T currentObj = objects.Count == 0 ? Object.Instantiate(obj) : objects.Pop();
            currentObj.transform.position = position;
            currentObj.gameObject.SetActive(true);
            return currentObj;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            objects.Push(obj);
        }

    }
    public class PoolWithMultipleItem<T> : IPool<T> where T : Component
    {
        private Dictionary<string, Stack<T>> objects = new();
        private List<T> objList;

        public PoolWithMultipleItem(List<T> instanceList)
        {
            objList = instanceList;
        }

        public T Request(Vector3 position)
        {
            T selectedObj = objList[Random.Range(0, objList.Count)];
            Stack<T> stack = GetObject(selectedObj.name);
            T currentObj = stack.Count == 0 ? Object.Instantiate(selectedObj) : GetObject(selectedObj.name).Pop();
            currentObj.name = selectedObj.name;
            currentObj.transform.position = position;
            currentObj.gameObject.SetActive(true);
            return currentObj;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            GetObject(obj.name).Push(obj);
        }

        public Stack<T> GetObject(string name)
        {
            if (!objects.ContainsKey(name))
            {
                objects.Add(name, new Stack<T>());
            }
            return objects[name];

        }
    }

}
