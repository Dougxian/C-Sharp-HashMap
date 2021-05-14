using System;
using System.Collections.Generic;
using System.Text;

namespace HashMap
{
    class Entry
    {
        /// <summary>
        /// 数据节点Key值
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 数据节点Value值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 数据节点指向下一节点指针
        /// </summary>
        public Entry next { get; set; }
        /// <summary>
        /// 节点数据Key的hash值
        /// </summary>
        public int hash { get; set; }
    }
}
