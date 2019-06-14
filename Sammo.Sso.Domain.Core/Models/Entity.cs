using System;

namespace Sammo.Sso.Domain.Core.Models
{
    /// <summary>
    /// 领域实体基类
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        /// <summary>
        /// 重写相等运算方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;
            if (ReferenceEquals(this, compareTo))
                return true;
            if (ReferenceEquals(null, compareTo))
                return false;
            return Id.Equals(compareTo.Id);
        }

        /// <summary>
        /// 获取hash
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }
    }
}
