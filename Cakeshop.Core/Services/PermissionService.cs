using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cakeshop.Core.Services.Interfaces;
using Cakeshop.DataLayer.Context;
using Cakeshop.DataLayer.Entities.Permissions;
using Cakeshop.DataLayer.Entities.User;

namespace Cakeshop.Core.Services
{
    public class PermissionService:IPermissionService
    {
        private CakeshopContext _context;

        public PermissionService(CakeshopContext context)
        {
            _context = context;
        }
        public List<Role> GetRoles()
        {
            return _context.CakeshopRoles.ToList();
        }
        public int AddRole(Role role)
        {
            _context.CakeshopRoles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }
        public Role GetRoleById(int roleId)
        {
            return _context.CakeshopRoles.Find(roleId);
        }

        public void UpdateRole(Role role)
        {
            _context.CakeshopRoles.Update(role);
            _context.SaveChanges();
        }
        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }
        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (int roleId in roleIds)
            {
                _context.CakeshopUserRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId
                });
            }

            _context.SaveChanges();
        }

        public void EditRolesUser(int userId, List<int> rolesId)
        {
            //Delete All Roles User
            _context.CakeshopUserRoles.Where(r=>r.UserId==userId).ToList().ForEach(r=> _context.CakeshopUserRoles.Remove(r));

            //Add New Roles
            AddRolesToUser(rolesId,userId);
        }

        public List<Permission> GetAllPermission()
        {
            return _context.Permission.ToList();
        }

        public void AddPermissionsToRole(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                _context.RolePermission.Add(new RolePermission()
                {
                    PermissionId = p,
                    RoleId = roleId
                });
            }

            _context.SaveChanges();
        }

        public List<int> PermissionsRole(int roleId)
        {
            return _context.RolePermission
                .Where(r => r.RoleId == roleId)
                .Select(r => r.PermissionId).ToList();
        }

        public void UpdatePermissionsRole(int roleId, List<int> permissions)
        {
            _context.RolePermission.Where(p => p.RoleId == roleId)
                .ToList().ForEach(p => _context.RolePermission.Remove(p));

            AddPermissionsToRole(roleId, permissions);
        }
        public bool CheckPermission(int permissionId, string userName)
        {
            int userId = _context.CakeshopUsers.Single(u => u.UserName == userName).UserId;

            List<int> userRoles = _context.CakeshopUserRoles
                .Where(r => r.UserId == userId).Select(r => r.RoleId).ToList();

            if (!userRoles.Any())
                return false;

            List<int> rolesPermission = _context.RolePermission
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId).ToList();

            return rolesPermission.Any(p => userRoles.Contains(p));


        }
    }
}
