using MyFragment.Business.Abstract;
using MyFragment.Entities.Entity;
using MyFragment.Entities.Entity.Enums;
using MyFragment.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Business.Manager
{
    public class UserManager : ManagerBase<User>, IManager
    {
        private static UserManager _userManager;
        private UserManager()
        {

        }
        public static UserManager CreateAsSingleton()
        {
            if (_userManager == null)
            {
                _userManager = new UserManager();
            }
            return _userManager;
        }
        public Result CreateUser(User user)
        {
            User userUsername = Find(I => I.Username == user.Username);
            User userEmail = Find(I => I.Email == user.Email);

            if (userUsername != null && userEmail != null)
            {
                return new Result() { ResultState = ResultState.UsernameEmailAlreadyExists, User = user };
            }
            else if (userUsername != null)
            {
                return new Result() { ResultState = ResultState.UsernameAlreadyExists, User = user };
            }
            else if (userEmail != null)
            {
                return new Result() { ResultState = ResultState.EmailAlreadyExists, User = user };
            }
            if (user.ImagePath == null)
            {
                user.ImagePath = "defaultPhoto.png";
            }
            if (Insert(user) > 0)
            {
                return new Result() { ResultState = ResultState.Success, User = Find(I => I.Username == user.Username) };
            }
            return new Result() { ResultState = ResultState.Error, User = user };
        }
        public Result EditUser(User user)
        {
            User userUsername = Find(I => I.Username == user.Username);
            User userEmail = Find(I => I.Email == user.Email);
            if (userEmail != null && userUsername != null)
            {
                if (userUsername.Id != user.Id && userEmail.Id != user.Id)
                {
                    return new Result() { ResultState = ResultState.UsernameEmailAlreadyExists, User = user };
                }
            }

            if (userUsername != null)
            {
                if (userUsername.Id != user.Id)
                {
                    return new Result() { ResultState = ResultState.UsernameAlreadyExists, User = user };
                }
            }

            if (userEmail != null)
            {
                if (userEmail.Id != user.Id)
                {
                    return new Result() { ResultState = ResultState.EmailAlreadyExists, User = user };
                }
            }

            if (user.ImagePath == null)
            {
                user.ImagePath = "defaultPhoto.png";
            }
            User editUser = Find(I => I.Id == user.Id);
            editUser.ImagePath = user.ImagePath;
            editUser.Name = user.Name;
            editUser.Surname = user.Surname;
            editUser.Username = user.Username;
            editUser.UserState = user.UserState;
            editUser.Password = user.Password;
            editUser.Email = user.Email;
            if (Save() >= 0)
            {
                return new Result() { ResultState = ResultState.Success, User = Find(I => I.Username == user.Username) };
            }
            return new Result() { ResultState = ResultState.Error, User = user };
        }
        public User ConvertToUser(RegisterViewModel registerViewModel)
        {
            return new User()
            {
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
                Username = registerViewModel.Username,
                UserState = UserState.StandartUser
            };
        }
        public Result Login(User user)
        {
            User findUser = Find(I => I.Username == user.Username && I.Password == user.Password);

            if (findUser == null)
            {
                return new Result() { ResultState = ResultState.NotFound, User = user };
            }

            return new Result() { ResultState = ResultState.Success, User = findUser };
        }
    }
}
