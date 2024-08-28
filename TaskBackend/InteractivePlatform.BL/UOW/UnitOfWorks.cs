
using InteractivePlatform.BL.Repository;

using InteractivePlatform.DAL.Model;

namespace InteractivePlatform.BL.UOW
{
    public class UnitOfWorks
    {
        InteractivePlatformContext db;
       
        GenericRepo<User> usersRepository;
        GenericRepo<FinanceRequest> financeRequestRepository;
       


        public UnitOfWorks(InteractivePlatformContext db)
        {
            this.db = db;

        }


        public GenericRepo<User> UsersRepository
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new GenericRepo<User>(db);

                }
                return usersRepository;
            }
        }

        public GenericRepo<FinanceRequest> FinanceRequestRepository
        {
            get
            {
                if (financeRequestRepository == null)
                {
                    financeRequestRepository = new GenericRepo<FinanceRequest>(db);

                }
                return financeRequestRepository;
            }
        }

        public void savechanges()
        {
            db.SaveChanges();
        }
    }
}

