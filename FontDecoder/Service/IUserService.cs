using FontDecoder.Model;

namespace FontDecoder.Service
{
    public interface IUserService
    {
        public bool Authorize(string username, string password,out User user);
        public User GetUser(string username);
        public int GetCredit(string username);
        public int SetCredit(string username,int number);
        public void AddUser(User user);
        public void RemoveUser(string username);
        void UpdateUser(User user);
    }
}
