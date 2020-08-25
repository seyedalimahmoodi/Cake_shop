using System;
using System.Collections.Generic;
using System.Linq;
using Cakeshop.Core.Convertors;
using Cakeshop.Core.DTOs;
using Cakeshop.Core.DTOs.Users;
using Cakeshop.Core.Generator;
using Cakeshop.Core.Security;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Context;
using Cakeshop.DataLayer.Entities.Order;
using Cakeshop.DataLayer.Entities.User;


namespace Cakeshop.Core.Services
{
    public class UserService : IUserService
    {
        private CakeshopContext _context;

        public UserService(CakeshopContext context)
        {
            _context = context;
        }


        public bool IsExistUserName(string userName)
        {
            return _context.CakeshopUsers.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return _context.CakeshopUsers.Any(u => u.Email == email);
        }

        public int AddUser(User user)
        {
            _context.CakeshopUsers.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixEmail(login.Email);
            return _context.CakeshopUsers.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.CakeshopUsers.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            _context.SaveChanges();

            return true;
        }
        public User GetUserById(int userId)
        {
            return _context.CakeshopUsers.Find(userId);
        }
        public int GetUserIdByUserName(string userName)
        {
            return _context.CakeshopUsers.Single(u => u.UserName == userName).UserId;
        }
        public User GetUserByEmail(string email)
        {
            return _context.CakeshopUsers.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.CakeshopUsers.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public User GetUserByUserName(string username)
        {
            return _context.CakeshopUsers.SingleOrDefault(u => u.UserName == username);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public InformationUserViewModel GetUserInformation(string username)
        {
            var user = GetUserByUserName(username);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            Order order= _context.Orders.SingleOrDefault(o => o.IsFinaly==false && o.UserId == user.UserId);
            if (order!=null)
            {
                information.Cart = order.OrderSum;
            }
            else
            {
                information.Cart = 0;
            }
            return information;

        }
        public InformationUserViewModel GetUserInformation(int userId)
        {
            var user = GetUserById(userId);
            InformationUserViewModel information = new InformationUserViewModel();
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            information.Cart = GetAmountCarts(user.UserName);

            return information;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanelData(string username)
        {
            return _context.CakeshopUsers.Where(u => u.UserName == username).Select(u =>
                new SideBarUserPanelViewModel()
                {
                    UserName = u.UserName,
                    RegisterDate = u.RegisterDate
                }).Single();
        }

        public EditProfileViewModel GetDataForEditProfileUser(string username)
        {
            return _context.CakeshopUsers.Where(u => u.UserName == username).Select(u => new EditProfileViewModel()
            {

                Email = u.Email,
                UserName = u.UserName

            }).Single();
        }

        public void EditProfile(string username, EditProfileViewModel profile)
        {

            var user = GetUserByUserName(username);
            user.UserName = profile.UserName;
            user.Email = profile.Email;

            UpdateUser(user);

        }

        public bool IsExistUserNameForEdit(string userName,string oldusername)
        {
            if (IsExistUserName(userName) && userName!= oldusername)
            {
                return true;

            }

            return false;
        }

        public bool IsExistEmailForEdit(string email, string username)
        {
            var user = GetUserByUserName(username);
            if (IsExistEmail(email) && email != user.Email)
            {
                return true;

            }

            return false;
        }
        public bool CompareOldPassword(string oldPassword, string username)
        {
            string hashOldPassword = PasswordHelper.EncodePasswordMd5(oldPassword);
            return _context.CakeshopUsers.Any(u => u.UserName == username && u.Password == hashOldPassword);
        }

        public void ChangeUserPassword(string userName, string newPassword)
        {
            var user = GetUserByUserName(userName);
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            UpdateUser(user);
        }
        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }
        public int GetAmountCarts(string username)
        {
            var userId = GetUserIdByUserName(username);
            return _context.Carts.Where(c => c.UserId == userId && c.IsPay == false).Select(c => c.Amount).ToList().Sum();
        }
        public List<CartViewModel> GetCartUser(string userName)
        {
            int userId = GetUserIdByUserName(userName);

            return _context.Carts
                .Where(w => w.IsPay && w.UserId == userId)
                .Select(w => new CartViewModel()
                {
                    Amount = w.Amount,
                    DateTime = w.CreateDate,
                    Description = w.Description,
                })
                .ToList();
        }
        public int ChargeCart(string userName, int amount, string description, bool isPay = false)
        {
            Cart cart = new Cart()
            {
                Amount = amount,
                CreateDate = DateTime.Now,
                Description = description,
                IsPay = isPay,
                UserId = GetUserIdByUserName(userName)
            };
          return  AddCart(cart);
        }
        public int AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return cart.CartId;
        }
        public Cart GetCartByCartId(int cartId)
        {
            return _context.Carts.Find(cartId);
        }

        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }


        public UserForAdminViewModel GetUsers(int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.CakeshopUsers;

            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                result = result.Where(u => u.UserName.Contains(filterUserName));
            }

            // Show Item In Page
            int take = 20;
            int skip = (pageId - 1) * take;


            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList();

            return list;
        }
        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _context.CakeshopUsers.Where(u => u.UserId == userId)
                .Select(u => new EditUserViewModel()
                {
                    UserId = u.UserId,
                    OldUserName = u.UserName,
                    Email = u.Email,
                    UserName = u.UserName,
                    UserRoles = u.UserRoles.Select(r => r.RoleId).ToList()
                }).Single();
        }

        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = GetUserById(editUser.UserId);
            user.Email = editUser.Email;
            user.UserName = editUser.UserName;
            if (!string.IsNullOrEmpty(editUser.UserName))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.UserName);
            }

            

            _context.CakeshopUsers.Update(user);
            _context.SaveChanges();
        }

        public void AddUserLocation(UserLocation location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        public List<UserLocation> Locations()
        {
            return _context.Locations.ToList();
        }
    }

}
