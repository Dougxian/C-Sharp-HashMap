using System;
using System.Collections.Generic;
using System.Text;

namespace HashMap
{
    public class HashMap : Map
    {
        //默认表的大小
        private const int defaultsize = 16;
        //表的大小
        private int size;
        //当前列表的数量，在添加或者删除元素的时候会更新
        private int modCount = 0;
        //表的数量，即桶的数量
        private Entry[] table;
        /// <summary>
        /// 默认大小实例化
        /// </summary>
        public HashMap() {
            this.table = new Entry[defaultsize];
        }
        /// <summary>
        /// 指定大小实例化
        /// </summary>
        /// <param name="size"></param>
        public HashMap(int size) {
            this.table = new Entry[size];
        }
        /// <summary>
        /// 获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string get(string key)
        {
            if (key == null) {
                return getForNullKey();
            }
            int hashnum = hash(key.GetHashCode());
            int index = indexFor(hashnum,table.Length);
            for (Entry e = table[index]; e != null; e = e.next) {
                string k = null;
                if (e.hash == hashnum && ((k = e.key) == key || key.Equals(k))){
                    return e.value;
                }
            }
            return null;
        }
        /// <summary>
        /// 加入value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string put(string key, string value)
        {
            //key 为null的时候将数据添加到table[0]，不需要计算哈希值
            if (key == null) {
                return putForNullKey(value);
            }
            int hashnum = hash(key.GetHashCode());
            int i = indexFor(hashnum,table.Length);
            for (Entry e = table[i]; e != null; e = e.next) {
                string k = null;
                if (e.hash == hashnum && ( (k = e.key) == key || key.Equals(k))) {
                    string oldValue = e.value;
                    e.value = value;
                    return oldValue;
                }
            }
            modCount++;
            addEntry(i, key, value, hashnum);
            return null;

        }
        /// <summary>
        /// 添加key为null的entry
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string putForNullKey(string value) {
            //循环遍历table[0]中元素
            for (Entry e = table[0]; e != null; e = e.next) {
                //找到key为null的元素
                if (e.key == null)
                {
                    string oldValue = e.value;
                    e.value = value;
                    return oldValue;
                }
            }
            //链表内的数量加一
            modCount++;
            addEntry(0, null, value,0);
            return null;
        }
        /// <summary>
        /// 获取key为null的Entry
        /// </summary>
        /// <returns></returns>
        private string getForNullKey() {
            for (Entry e = table[0]; e != null; e = e.next) {
                if (e.key == null) {
                    return e.value;
                }
            }
            return null;
        }
        /// <summary>
        /// 在指定的table里面添加entry
        /// </summary>
        /// <param name="table"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>新添加元素的value</returns>
        private string addEntry(int table,string key,string value,int hash) {
            var e = this.table[table];
            Entry newe = new Entry()
            {
                key = key,
                value = value,
                next = e,
                hash = hash
            };
            this.table[table] = newe;
            return newe.value;
        }
        /// <summary>
        /// 规范hash值
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        static int hash(int h) {
            h ^= (h >> 20) ^ (h >> 12);
            return h ^ (h >> 7) ^ (h >> 4);
        }
        /// <summary>
        /// 确定目标桶
        /// </summary>
        /// <param name="h"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        static int indexFor(int h, int length) {
            return h & (length - 1);
        }
    }
}
