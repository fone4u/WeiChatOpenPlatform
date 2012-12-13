using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainCore.DB;

namespace DomainCore.Filters
{
    class LocationFilters : IFilter
    {
        public bool Filters(string userId, string content, out  string result)
        {
            User user = DataBase.Instance.Users.SingleOrDefault(t => t.ID == userId);
            if (user != null)
            {
                if (content == "上海" || content == "shanghai")
                {
                    Transaction transaction = DataBase.Instance.Transaction.SingleOrDefault(t => t.UserId == userId);
                    if (transaction != null && transaction.Status == Status.KeyWordSet)
                    {
                        transaction.Location = content;
                        transaction.Status = Status.Done;
                        result = "这里是职位结果";
                        return true;
                    }
                }

            }
            result = string.Empty;
            return false;
        }
    }
}
