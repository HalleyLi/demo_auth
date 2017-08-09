using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Core
{
    public class TicketContainer
    {
        private ObjectCache _tickets;
        private CacheItemPolicy _policy;
        private static readonly int _refreshTicketInterval = AppconfigWrapper.DetectTicketOnlineInterval;

        private TicketContainer()
        {
            _tickets = new MemoryCache("TICKETS");
            _policy = new CacheItemPolicy();
            if (_refreshTicketInterval > 0)
            {
                _policy.SlidingExpiration = TimeSpan.FromSeconds(_refreshTicketInterval);
            }
        }

        public static TicketContainer Instance
        {
            get { return TicketContainerCreator._ticketContainer; }
        }

        public void AddTicket(string ticket, WapApp app, string clientKey, WapUser user)
        {
            string appIdentity = app.AppIdentity;
            string key = this.GetKey(ticket);

            var now = DateTime.Now;
            TicketObject ticketObj = new TicketObject();
            ticketObj.User = user;
            ticketObj.Ticket = ticket;
            ticketObj.Timeout = now;//暂时没有timeout的逻辑
            ticketObj.LastHeartbeat = now;
            ticketObj.ClientKey = clientKey;
            ticketObj.App = app;
            _tickets.Add(key.ToString(), ticketObj, _policy);
            //??? 数据库???
        }

        public void RemoveTicket(string ticket)
        {
            string key = this.GetKey(ticket);
            _tickets.Remove(key);
        }

        public bool IsTicketValid(string ticket, string clientKey)
        {
            string key = this.GetKey(ticket);
            if (_tickets.Contains(key))
            {
                var item = _tickets[key] as TicketObject;
                if (string.Compare(clientKey, item.ClientKey, true) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public TicketObject GetTicket(string ticket, string appIdentity)
        {
            string key = this.GetKey(ticket);
            return _tickets[key] as TicketObject;
        }

        /// <summary>
        /// Ping ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="appIdentity"></param>
        public void PingTicket(string ticket, string appIdentity)
        {
            string key = GetKey(ticket);
            TicketObject item = _tickets[key] as TicketObject;
            if (item != null)
            {
                item.LastHeartbeat = DateTime.Now;
            }
        }

        /// <summary>
        /// 更加ticket 获取用户信息
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public WapUser GetUserByTicket(string ticket)
        {
            WapUser user = null;
            string key = GetKey(ticket);
            var item = _tickets[key] as TicketObject;
            if (item != null)
            {
                user = item.User;
            }
            return user;
        }

        /// <summary>
        /// 获取应用程序在线用户数
        /// </summary>
        /// <param name="appIdentity"></param>
        /// <returns></returns>
        public List<WapUser> GetAppOnlineUsers(string appIdentity)
        {
            List<WapUser> users = new List<WapUser>();
            foreach (var item in _tickets)
            {
                TicketObject ticket = item.Value as TicketObject;
                if (string.Compare(appIdentity, ticket.App.AppIdentity, true) == 0)
                {
                    users.Add(ticket.User);
                }
            }

            return users;
        }

        public string[] GetLogOnApps(string account)
        {

            List<string> appIdentities = new List<string>();
            foreach (var item in _tickets)
            {
                TicketObject ticket = item.Value as TicketObject;
                if (string.Compare(account, ticket.User.Account, true) == 0)
                {
                    appIdentities.Add(ticket.App.AppIdentity);
                }
            }

            return appIdentities.Distinct().ToArray();
        }

        public List<TicketObject> GetAllItems()
        {
            List<TicketObject> recList = new List<TicketObject>();
            foreach (var item in _tickets)
            {
                var ticket = item.Value as TicketObject;
                var copyValue = ticket.Copy();
                recList.Add(copyValue);
            }

            return recList;
        }

        private string GetKey(string ticket)
        {
            return ticket;
        }

        private class TicketContainerCreator
        {
            internal static TicketContainer _ticketContainer;

            static TicketContainerCreator()
            {
                _ticketContainer = new TicketContainer();
            }
        }
    }
}
