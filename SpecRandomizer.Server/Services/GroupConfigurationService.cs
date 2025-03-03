using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpecRandomizer.Server.Migrations;
using SpecRandomizer.Server.Model;
using System.Data;
using System;
using System.Linq;

namespace SpecRandomizer.Server.Services
{
    public class GroupConfigurationService
    {

        private List<Role> GetPossibleRoles(List<ClassList> availableClasses)
        {
            var roles = new HashSet<Role>();
            foreach (var cls in availableClasses)
            {
                foreach (var kvp in ClassMappings.Specializations)
                {
                    if (kvp.Key.Item1 == cls)
                        roles.Add(kvp.Key.Item2);
                }
            }
            return roles.ToList();
        }

        private Role SelectRole(List<Role> possibleRoles, int tankCount, int healerCount, int damageCount)
        {
            Role assignedRole = Role.INVALID;
            if (tankCount < 1 && possibleRoles.Contains(Role.TANK))
                assignedRole = Role.TANK;
            else if (healerCount < 1 && possibleRoles.Contains(Role.HEALER))
                assignedRole = Role.HEALER;
            else if (damageCount < 3 && possibleRoles.Contains(Role.DAMAGE))
                assignedRole = Role.DAMAGE;

            return assignedRole;
        }

        private Specialization AssignSpecialization(List<ClassList> availableClasses, Role assignedRole)
        {
            var rng = new Random();
            var possibleSpecs = availableClasses
             .Where(cls => ClassMappings.Specializations.ContainsKey((cls, assignedRole)))
             .SelectMany(cls => ClassMappings.Specializations[(cls, assignedRole)])
             .ToList();




            return possibleSpecs.Count > 0 ? possibleSpecs[rng.Next(possibleSpecs.Count)] : ClassMappings.Specializations[(ClassList.NONE, Role.INVALID)].First();
        }

        public List<RoleAssignment> GetRoleAssignments(Configuration config)
        {
            var assignments = new List<RoleAssignment>();
            var availablePlayers = new List<Player>(config.Players);

            if (availablePlayers.Count == 0) return assignments;

            var rng = new Random();
            availablePlayers = [.. availablePlayers.OrderBy(_ => rng.Next())];

            int tankCount = 0, healerCount = 0, damageCount = 0;

            foreach (var player in availablePlayers)
            {
                var possibleRoles = GetPossibleRoles(player.SpecList);
                if (possibleRoles.Count == 0)
                {
                    List<ClassList> dummy = new();
                    dummy.Add(ClassList.NONE);
                    assignments.Add(new RoleAssignment(player, AssignSpecialization(dummy, Role.INVALID)));
                }
                Role assignedRole = Role.INVALID;
                double randomSelector = rng.NextDouble();
                if (randomSelector < .33 || damageCount >= 3)
                {
                    if (possibleRoles.Contains(Role.TANK) && tankCount < 0)
                    {
                        assignedRole = Role.TANK;
                    }
                    else
                    {
                        assignedRole = SelectRole(possibleRoles, tankCount, healerCount, damageCount);
                    }

                }
                else if (randomSelector > .33 || damageCount >= 3)
                {
                    if (possibleRoles.Contains(Role.HEALER) && tankCount < 0)
                    {
                        assignedRole = Role.HEALER;
                    }
                    else
                    {
                        assignedRole = SelectRole(possibleRoles, tankCount, healerCount, damageCount);
                    }
                }
                else
                {
                    if (damageCount < 3)
                    {
                        assignedRole = Role.DAMAGE;
                    }
                    else
                    {
                        assignedRole = SelectRole(possibleRoles, tankCount, healerCount, damageCount);
                    }
                }

                if (assignedRole != Role.INVALID)
                {
                    if (assignedRole == Role.TANK) tankCount++;
                    if (assignedRole == Role.HEALER) healerCount++;
                    if (assignedRole == Role.DAMAGE) damageCount++;

                    assignments.Add(new RoleAssignment(player, AssignSpecialization(player.SpecList, assignedRole)));
                }
            }


            return assignments;
        }
    }
}
