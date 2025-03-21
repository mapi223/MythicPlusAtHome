using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using SpecRandomizer.Server.Model;

namespace SpecRandomizer.Server.Model

{

    public enum Role
    {
        TANK,
        HEALER,
        DAMAGE,
        INVALID
    }
    public enum ClassList
    {
        DEATH_KNIGHT,
        DEMON_HUNTER,
        DRUID,
        EVOKER,
        HUNTER,
        MAGE,
        MONK,
        PALADIN,
        PRIEST,
        ROGUE,
        SHAMAN,
        WARLOCK,
        WARRIOR,
        NONE
    }

    public class Specialization
    {
        public Specialization(ClassList Class, Role role, string name)
        {
            Special = Class;
            Role = role;
            Name = name;
        }
        public ClassList Special { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
    }

    public static class ClassMappings
    {
        //defines the roles allowed Per ClassList
        public static readonly Dictionary<ClassList, List<Role>> AllowedRoles = new()
        {
            { ClassList.DEATH_KNIGHT, new List<Role> {Role.TANK, Role.DAMAGE } },
            { ClassList.DEMON_HUNTER, new List<Role> {Role.TANK, Role.DAMAGE } },
            { ClassList.DRUID, new List<Role> {Role.TANK, Role.HEALER, Role.DAMAGE } },
            { ClassList.EVOKER, new List<Role> { Role.HEALER, Role.DAMAGE } },
            { ClassList.HUNTER, new List<Role> { Role.DAMAGE } },
            { ClassList.MAGE, new List<Role> {Role.DAMAGE } },
            { ClassList.MONK, new List<Role> {Role.TANK, Role.HEALER, Role.DAMAGE } },
            { ClassList.PALADIN, new List<Role> {Role.TANK, Role.HEALER, Role.DAMAGE } },
            { ClassList.PRIEST, new List<Role> {Role.HEALER, Role.DAMAGE } },
            { ClassList.ROGUE, new List<Role> {Role.DAMAGE } },
            { ClassList.SHAMAN, new List<Role> {Role.HEALER, Role.DAMAGE } },
            { ClassList.WARLOCK, new List<Role> {Role.DAMAGE } },
            { ClassList.WARRIOR, new List<Role> {Role.TANK, Role.DAMAGE } }
        };

        public static readonly Dictionary<(ClassList, Role), List<Specialization>> Specializations = new()
        {
            { (ClassList.DEATH_KNIGHT, Role.TANK), new List <Specialization> {  new Specialization(ClassList.DEATH_KNIGHT, Role.TANK, "Blood Death Knight") } },
            { (ClassList.DEATH_KNIGHT, Role.DAMAGE), new List <Specialization> {new Specialization(ClassList.DEATH_KNIGHT, Role.DAMAGE, "Frost Death Knight" ), 
                                                                                new Specialization(ClassList.DEATH_KNIGHT, Role.DAMAGE, "Unholy Death Knight") } },
            { (ClassList.DEMON_HUNTER, Role.TANK), new List <Specialization> {  new Specialization(ClassList.DEMON_HUNTER, Role.TANK, "Vengeance Demon Hunter") } },
            { (ClassList.DEMON_HUNTER, Role.DAMAGE), new List <Specialization> {new Specialization(ClassList.DEMON_HUNTER, Role.DAMAGE, "Havoc Demon Hunter") } },
            { (ClassList.DRUID, Role.TANK), new List <Specialization> {         new Specialization(ClassList.DRUID, Role.TANK, "Guardian Druid") } },
            { (ClassList.DRUID, Role.HEALER),new List <Specialization> {        new Specialization(ClassList.DRUID, Role.HEALER, "Restoration Druid") } },
            { (ClassList.DRUID, Role.DAMAGE), new List <Specialization> {       new Specialization(ClassList.DEATH_KNIGHT, Role.DAMAGE, "Feral Druid"), 
                                                                                new Specialization(ClassList.DRUID, Role.DAMAGE, "Balance Druid") } },
            { (ClassList.EVOKER, Role.HEALER), new List <Specialization>  {     new Specialization(ClassList.EVOKER, Role.HEALER, "Presevation Evoker") } },
            { (ClassList.EVOKER, Role.DAMAGE),new List <Specialization>  {      new Specialization(ClassList.EVOKER, Role.HEALER, "Devestation Evoker"),
                                                                                new Specialization(ClassList.EVOKER, Role.HEALER, "Augmentation Evoker") } },
            { (ClassList.HUNTER, Role.DAMAGE), new List <Specialization> {      new Specialization(ClassList.HUNTER, Role.DAMAGE, "Beast Master Hunter"), 
                                                                                new Specialization(ClassList.HUNTER, Role.DAMAGE, "Marksmenship Hunter"),
                                                                                new Specialization(ClassList.HUNTER, Role.DAMAGE, "Survival Hunter") } },
            { (ClassList.MAGE, Role.DAMAGE), new List <Specialization> {        new Specialization(ClassList.MAGE, Role.DAMAGE, "Fire Mage"), 
                                                                                new Specialization(ClassList.MAGE, Role.DAMAGE, "Frost Mage"),   
                                                                                new Specialization(ClassList.MAGE, Role.DAMAGE, "Arcane Mage") } },
            { (ClassList.MONK, Role.TANK), new List <Specialization> {          new Specialization(ClassList.MONK, Role.TANK, "Brewmaster Monk") } },
            { (ClassList.MONK, Role.HEALER),new List <Specialization> {         new Specialization(ClassList.MONK, Role.HEALER, "Mistweaver Monk") } },
            { (ClassList.MONK, Role.DAMAGE), new List <Specialization> {        new Specialization(ClassList.MONK, Role.DAMAGE, "Windwalker Monk") } },
            { (ClassList.PRIEST, Role.HEALER), new List <Specialization>  {     new Specialization(ClassList.PRIEST, Role.HEALER, "Holy Priest") , 
                                                                                new Specialization(ClassList.PRIEST, Role.HEALER, "Disc Priest") } },
            { (ClassList.PRIEST, Role.DAMAGE),new List <Specialization>  {      new Specialization(ClassList.PRIEST, Role.DAMAGE, "Shadow Priest") } },
            { (ClassList.PALADIN, Role.TANK), new List <Specialization> {       new Specialization(ClassList.PALADIN, Role.TANK, "Protection Paladin") } },
            { (ClassList.PALADIN, Role.HEALER),new List <Specialization>  {     new Specialization(ClassList.PALADIN, Role.HEALER, "Holy Paladin") } },
            { (ClassList.PALADIN, Role.DAMAGE),new List <Specialization> {      new Specialization(ClassList.PALADIN, Role.DAMAGE, "Retribution Paladin") } },
            { (ClassList.ROGUE, Role.DAMAGE), new List <Specialization> {       new Specialization(ClassList.ROGUE, Role.DAMAGE, "Assassination Rogue"), 
                                                                                new Specialization(ClassList.ROGUE, Role.DAMAGE, "Outlaw Rogue"), 
                                                                                new Specialization(ClassList.ROGUE, Role.DAMAGE, "Subtlety Rogue") } },
            { (ClassList.SHAMAN, Role.HEALER), new List <Specialization>  {     new Specialization(ClassList.SHAMAN, Role.HEALER, "Restoration Shaman") } },
            { (ClassList.SHAMAN, Role.DAMAGE),new List <Specialization>  {      new Specialization(ClassList.SHAMAN, Role.DAMAGE, "Elemental Shaman"), 
                                                                                new Specialization(ClassList.SHAMAN, Role.DAMAGE, "Enhancement Shaman") } },
            { (ClassList.WARLOCK, Role.DAMAGE), new List <Specialization> {     new Specialization(ClassList.WARLOCK, Role.DAMAGE, "Affliction Warlock"),
                                                                                new Specialization(ClassList.WARLOCK, Role.DAMAGE, "Demonology Warlock"), 
                                                                                new Specialization(ClassList.WARLOCK, Role.DAMAGE, "Destruction Warlock") } },
            { (ClassList.WARRIOR, Role.TANK), new List <Specialization> {       new Specialization(ClassList.WARRIOR, Role.TANK, "Protection Warrior") } },
            { (ClassList.WARRIOR, Role.DAMAGE), new List <Specialization> {     new Specialization(ClassList.WARRIOR, Role.DAMAGE, "Arms Warrior"),
                                                                                new Specialization(ClassList.WARRIOR, Role.DAMAGE, "Fury Warrior") } },
            { (ClassList.NONE, Role.INVALID), new List <Specialization> {       new Specialization(ClassList.NONE, Role.INVALID, "Some Weirdness happening") } }

        };
    }

    public class RoleAssignment
    {
        public RoleAssignment(Player player, Specialization specialization)
        {
            Player = player;
            AssignedSpec = specialization;
        }

        public Player Player { get; set; }
        public Specialization AssignedSpec { get; set; }
    }

    public class RoleAssignmentDto
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string SpecName { get; set; }


        public static List<RoleAssignmentDto> ConvertToDtoList(List<RoleAssignment> roleAssignments)
        {
            return [.. roleAssignments.Select(ra => new RoleAssignmentDto
            {
                PlayerId = ra.Player.PlayerId,
                PlayerName = ra.Player.PlayerName,
                SpecName = ra.AssignedSpec.Name
            })];
        }
    }
}
