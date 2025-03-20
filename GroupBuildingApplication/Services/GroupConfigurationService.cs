using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System;
using System.Linq;
using GroupBuildingService.Models;

namespace GroupBuildingService.Services
{
    public class GroupConfigurationService
    {

        private List<PlayerRole> GetPossibleRoles(List<ClassList> availableClasses)
        {
            var roles = new HashSet<PlayerRole>();
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

        private PlayerRole SelectRole(List<PlayerRole> possibleRoles, int tankCount, int healerCount, int damageCount)
        {
            PlayerRole assignedRole = PlayerRole.INVALID;
            if (tankCount < 1 && possibleRoles.Contains(PlayerRole.TANK))
                assignedRole = PlayerRole.TANK;
            else if (healerCount < 1 && possibleRoles.Contains(PlayerRole.HEALER))
                assignedRole = PlayerRole.HEALER;
            else if (damageCount < 3 && possibleRoles.Contains(PlayerRole.DAMAGE))
                assignedRole = PlayerRole.DAMAGE;

            return assignedRole;
        }

        private Specialization AssignSpecialization(List<ClassList> availableClasses, PlayerRole assignedRole)
        {
            var rng = new Random();
            var possibleSpecs = availableClasses
             .Where(cls => ClassMappings.Specializations.ContainsKey((cls, assignedRole)))
             .SelectMany(cls => ClassMappings.Specializations[(cls, assignedRole)])
             .ToList();




            return possibleSpecs.Count > 0 ? possibleSpecs[rng.Next(possibleSpecs.Count)] : ClassMappings.Specializations[(ClassList.NONE, PlayerRole.INVALID)].First();
        }

        public List<RoleAssignmentDto> GetRoleAssignments(Configuration config)
        {
            var assignments = new List<RoleAssignment>();
            var availablePlayers = new List<Player>(config.Players);

            if (availablePlayers.Count == 0) return RoleAssignmentDto.ConvertToDtoList(assignments);

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
                    assignments.Add(new RoleAssignment(player, AssignSpecialization(dummy, PlayerRole.INVALID)));
                }
                PlayerRole assignedRole = PlayerRole.INVALID;
                double randomSelector = rng.NextDouble();
                if (randomSelector < .33 || damageCount >= 3)
                {
                    if (possibleRoles.Contains(PlayerRole.TANK) && tankCount < 1)
                    {
                        assignedRole = PlayerRole.TANK;
                    }
                    else
                    {
                        assignedRole = SelectRole(possibleRoles, tankCount, healerCount, damageCount);
                    }

                }
                else if (randomSelector > .33 || damageCount >= 3)
                {
                    if (possibleRoles.Contains(PlayerRole.HEALER) && healerCount < 1)
                    {
                        assignedRole = PlayerRole.HEALER;
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
                        assignedRole = PlayerRole.DAMAGE;
                    }
                    else
                    {
                        assignedRole = SelectRole(possibleRoles, tankCount, healerCount, damageCount);
                    }
                }

                if (assignedRole != PlayerRole.INVALID)
                {
                    if (assignedRole == PlayerRole.TANK) tankCount++;
                    if (assignedRole == PlayerRole.HEALER) healerCount++;
                    if (assignedRole == PlayerRole.DAMAGE) damageCount++;

                    assignments.Add(new RoleAssignment(player, AssignSpecialization(player.SpecList, assignedRole)));
                }
            }
            return RoleAssignmentDto.ConvertToDtoList(assignments);
        }
    }
}
