using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using System;

namespace COAssistance.API.Services.Logger
{
    public class DbLoggerService : ILoggerService
    {
        private COAssistanceDbContext dbContext;

        public DbLoggerService(COAssistanceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Log(string userId, ActionType action, string description)
        {
            dbContext.UserAudit.Add(new UserAudit
            {
                ActionType = GetActionDescription(action),
                AdminId = userId,
                DateTime = DateTime.UtcNow,
                Description = description
            });

            dbContext.SaveChanges();
        }

        private string GetActionDescription(ActionType action)
        {
            switch (action)
            {
                case ActionType.Create:
                    return "Create";

                case ActionType.Update:
                    return "Update";

                case ActionType.Delete:
                    return "Delete";
            }

            throw new NotImplementedException("Not supported logging action type");
        }
    }
}