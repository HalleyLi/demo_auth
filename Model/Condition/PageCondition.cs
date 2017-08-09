using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.BM.Model.Dto
{
    /// <summary>
    /// 查询条件Dto
    /// </summary>
    public class PageCondition
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="start">开始记录索引</param>
        /// <param name="length">本次提取记录数量</param>
        public PageCondition(int start, int length)
        {
            this.Start = start;
            this.Length = length;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="start">开始记录索引</param>
        /// <param name="length">本次提取记录数量</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="sortDirection">排序方式</param>
        public PageCondition(int start, int length, string[] sortField, string[] sortDirection)
        {
            this.Start = start;
            this.Length = length;
            this.SortField = sortField;
            this.SortDirection = sortDirection;
        }

        /// <summary>
        /// 显示的起始索引 
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 显示的行数
        /// </summary>
        public int Length { get; set; }

        ///// <summary>
        ///// 全局搜索字段
        ///// </summary>
        //public string SearchText { get; set; }

        /// <summary>
        /// 被排序的列
        /// </summary>
        public string[] SortField { get; set; }

        /// <summary>
        /// asc or desc
        /// </summary>
        public string[] SortDirection { get; set; }

        public enum DirectionWord
        {
            /// <summary>
            /// 倒序
            /// </summary>
            Desc,
            
            /// <summary>
            /// 正序
            /// </summary>
            Asc
        }

        /// <summary>
        /// 验证分页信息
        /// Start > 0,
        /// Length Between 10, 5000;
        /// SortFiled.Length == SortDirection.Length;
        /// </summary>
        /// <returns>True: 验证通过; False:验证失败</returns>
        public bool Validate()
        {
            // 开始记录索引小于0
            if (this.Start <= 0)
                return false;

            // 长度必须在10-5000之间
            if (this.Length < 10 || this.Length > 5000)
                return false;

            // SortDirection或SortField必须同时提供
            if ((this.SortDirection == null && this.SortField != null) ||
                (this.SortField == null && this.SortDirection != null))
                return false;

            // SortField, SortDirection的数量不相等
            if(this.SortDirection.Length != this.SortField.Length)
                return false;

            return true;
        }
    }
}
