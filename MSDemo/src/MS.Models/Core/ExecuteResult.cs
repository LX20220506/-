using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Models.Core
{
    //这个类是定义了业务相关方法调用的返回值结果，包含了是否成功、错误信息，
    //也可以拓展泛型夹带其他内容
    /// <summary>
    /// 执行返回结果
    /// </summary>
    public class ExecuteResult 
    {
        /// <summary>
        /// 执行是否为空
        /// 默认为true
        /// </summary>
        public bool IsSucceed { get; set; }=true;


        /// <summary>
        /// 执行消息（一般是错误信息）
        /// 默认为空
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 设置执行返回结果
        /// </summary>
        /// <param name="isSucceed">执行结果</param>
        /// <param name="message">执行消息</param>
        /// <returns>返回执行结果</returns>
        public virtual ExecuteResult Set(bool isSucceed,string message) {
            IsSucceed = isSucceed;
            Message = message;
            return this;
        }


        /// <summary>
        /// 设置错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <returns></returns>
        public virtual ExecuteResult SetFailMessage(string message) {
            return Set(false, message);
        }
        public virtual ExecuteResult SetFail() { 
            return Set(false, string.Empty);
        }

        public ExecuteResult(bool isSucceed,string message) {
            Set(isSucceed,message);
        }

        /// <summary>
        /// 如果是给字符串，表示有错误信息，默认IsSucceed=false
        /// </summary>
        /// <param name="message"></param>
        public ExecuteResult(string message) {
            Set(false, message);
        }

        /// <summary>
        /// 如果是空的，没有信息，默认IsSucceed=true
        /// </summary>
        public ExecuteResult()
        {
        }
    }


    public class ExecuteResult<T> : ExecuteResult {

        public T Result { get; set; }

        public ExecuteResult<T> Set(bool isSucceed,string message,T result) { 
            IsSucceed=isSucceed;
            Message=message;
            Result = result;
            return this;
        }

        public ExecuteResult<T> SetData(T data) {
            return Set(true, string.Empty, data);
        }

        public new ExecuteResult<T> SetFail() {
            return Set(false,string.Empty,default);
        }


        public new ExecuteResult<T> SetFailMessage(string message) {
            return Set(false,message,default);
        }

        public ExecuteResult()
        {
        }

        public ExecuteResult(string message) {
            Set(false, message);
        }

        public ExecuteResult(bool isSucceed, string message) {
            Set(isSucceed, message);
        }

        public ExecuteResult(T result)
        {
            SetData(result);
        }
    }
}
