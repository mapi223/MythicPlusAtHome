using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

namespace GroupBuildingService.Models

{

    public enum PlayerRole
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
        public Specialization(ClassList Class, PlayerRole role, string name)
        {
            Special = Class;
            Role = role;
            Name = name;
        }
        public ClassList Special { get; set; }
        public PlayerRole Role { get; set; }
        public string Name { get; set; }
    }

    public static class ClassMappings
    {
        //defines the roles allowed Per ClassList
        public static readonly Dictionary<ClassList, List<PlayerRole>> AllowedRoles = new()
        {
            { ClassList.DEATH_KNIGHT, new List<PlayerRole> {PlayerRole.TANK, PlayerRole.DAMAGE } },
            { ClassList.DEMON_HUNTER, new List<PlayerRole> {PlayerRole.TANK, PlayerRole.DAMAGE } },
            { ClassList.DRUID, new List<PlayerRole> {PlayerRole.TANK, PlayerRole.HEALER, PlayerRole.DAMAGE } },
            { ClassList.EVOKER, new List<PlayerRole> { PlayerRole.HEALER, PlayerRole.DAMAGE } },
            { ClassList.HUNTER, new List<PlayerRole> { PlayerRole.DAMAGE } },
            { ClassList.MAGE, new List<PlayerRole> {PlayerRole.DAMAGE } },
            { ClassList.MONK, new List<PlayerRole> {PlayerRole.TANK, PlayerRole.HEALER, PlayerRole.DAMAGE } },
            { ClassList.PALADIN, new List<PlayerRole> {PlayerRole.TANK, PlayerRole.HEALER, PlayerRole.DAMAGE } },
            { ClassList.PRIEST, new List<PlayerRole> {PlayerRole.HEALER, PlayerRole.DAMAGE } },
            { ClassList.ROGUE, new List<PlayerRole> {PlayerRole.DAMAGE } },
            { ClassList.SHAMAN, new List<PlayerRole> {PlayerRole.HEALER, PlayerRole.DAMAGE } },
            { ClassList.WARLOCK, new List<PlayerRole> {PlayerRole.DAMAGE } },
            { ClassList.WARRIOR, new List<PlayerRole> {PlayerRole.TANK, PlayerRole.DAMAGE } }
        };

        public static readonly Dictionary<(ClassList, PlayerRole), List<Specialization>> Specializations = new()
        {
            { (ClassList.DEATH_KNIGHT, PlayerRole.TANK), new List <Specialization> {  new Specialization(ClassList.DEATH_KNIGHT, PlayerRole.TANK, "Blood Death Knight") } },
            { (ClassList.DEATH_KNIGHT, PlayerRole.DAMAGE), new List <Specialization> {new Specialization(ClassList.DEATH_KNIGHT, PlayerRole.DAMAGE, "Frost Death Knight" ), 
                                                                                new Specialization(ClassList.DEATH_KNIGHT, PlayerRole.DAMAGE, "Unholy Death Knight") } },
            { (ClassList.DEMON_HUNTER, PlayerRole.TANK), new List <Specialization> {  new Specialization(ClassList.DEMON_HUNTER, PlayerRole.TANK, "Vengeance Demon Hunter") } },
            { (ClassList.DEMON_HUNTER, PlayerRole.DAMAGE), new List <Specialization> {new Specialization(ClassList.DEMON_HUNTER, PlayerRole.DAMAGE, "Havoc Demon Hunter") } },
            { (ClassList.DRUID, PlayerRole.TANK), new List <Specialization> {         new Specialization(ClassList.DRUID, PlayerRole.TANK, "Guardian Druid") } },
            { (ClassList.DRUID, PlayerRole.HEALER),new List <Specialization> {        new Specialization(ClassList.DRUID, PlayerRole.HEALER, "Restoration Druid") } },
            { (ClassList.DRUID, PlayerRole.DAMAGE), new List <Specialization> {       new Specialization(ClassList.DEATH_KNIGHT, PlayerRole.DAMAGE, "Feral Druid"), 
                                                                                new Specialization(ClassList.DRUID, PlayerRole.DAMAGE, "Balance Druid") } },
            { (ClassList.EVOKER, PlayerRole.HEALER), new List <Specialization>  {     new Specialization(ClassList.EVOKER, PlayerRole.HEALER, "Presevation Evoker") } },
            { (ClassList.EVOKER, PlayerRole.DAMAGE),new List <Specialization>  {      new Specialization(ClassList.EVOKER, PlayerRole.HEALER, "Devestation Evoker"),
                                                                                new Specialization(ClassList.EVOKER, PlayerRole.HEALER, "Augmentation Evoker") } },
            { (ClassList.HUNTER, PlayerRole.DAMAGE), new List <Specialization> {      new Specialization(ClassList.HUNTER, PlayerRole.DAMAGE, "Beast Master Hunter"), 
                                                                                new Specialization(ClassList.HUNTER, PlayerRole.DAMAGE, "Marksmenship Hunter"),
                                                                                new Specialization(ClassList.HUNTER, PlayerRole.DAMAGE, "Survival Hunter") } },
            { (ClassList.MAGE, PlayerRole.DAMAGE), new List <Specialization> {        new Specialization(ClassList.MAGE, PlayerRole.DAMAGE, "Fire Mage"), 
                                                                                new Specialization(ClassList.MAGE, PlayerRole.DAMAGE, "Frost Mage"),   
                                                                                new Specialization(ClassList.MAGE, PlayerRole.DAMAGE, "Arcane Mage") } },
            { (ClassList.MONK, PlayerRole.TANK), new List <Specialization> {          new Specialization(ClassList.MONK, PlayerRole.TANK, "Brewmaster Monk") } },
            { (ClassList.MONK, PlayerRole.HEALER),new List <Specialization> {         new Specialization(ClassList.MONK, PlayerRole.HEALER, "Mistweaver Monk") } },
            { (ClassList.MONK, PlayerRole.DAMAGE), new List <Specialization> {        new Specialization(ClassList.MONK, PlayerRole.DAMAGE, "Windwalker Monk") } },
            { (ClassList.PRIEST, PlayerRole.HEALER), new List <Specialization>  {     new Specialization(ClassList.PRIEST, PlayerRole.HEALER, "Holy Priest") , 
                                                                                new Specialization(ClassList.PRIEST, PlayerRole.HEALER, "Disc Priest") } },
            { (ClassList.PRIEST, PlayerRole.DAMAGE),new List <Specialization>  {      new Specialization(ClassList.PRIEST, PlayerRole.DAMAGE, "Shadow Priest") } },
            { (ClassList.PALADIN, PlayerRole.TANK), new List <Specialization> {       new Specialization(ClassList.PALADIN, PlayerRole.TANK, "Protection Paladin") } },
            { (ClassList.PALADIN, PlayerRole.HEALER),new List <Specialization>  {     new Specialization(ClassList.PALADIN, PlayerRole.HEALER, "Holy Paladin") } },
            { (ClassList.PALADIN, PlayerRole.DAMAGE),new List <Specialization> {      new Specialization(ClassList.PALADIN, PlayerRole.DAMAGE, "Retribution Paladin") } },
            { (ClassList.ROGUE, PlayerRole.DAMAGE), new List <Specialization> {       new Specialization(ClassList.ROGUE, PlayerRole.DAMAGE, "Assassination Rogue"), 
                                                                                new Specialization(ClassList.ROGUE, PlayerRole.DAMAGE, "Outlaw Rogue"), 
                                                                                new Specialization(ClassList.ROGUE, PlayerRole.DAMAGE, "Subtlety Rogue") } },
            { (ClassList.SHAMAN, PlayerRole.HEALER), new List <Specialization>  {     new Specialization(ClassList.SHAMAN, PlayerRole.HEALER, "Restoration Shaman") } },
            { (ClassList.SHAMAN, PlayerRole.DAMAGE),new List <Specialization>  {      new Specialization(ClassList.SHAMAN, PlayerRole.DAMAGE, "Elemental Shaman"), 
                                                                                new Specialization(ClassList.SHAMAN, PlayerRole.DAMAGE, "Enhancement Shaman") } },
            { (ClassList.WARLOCK, PlayerRole.DAMAGE), new List <Specialization> {     new Specialization(ClassList.WARLOCK, PlayerRole.DAMAGE, "Affliction Warlock"),
                                                                                new Specialization(ClassList.WARLOCK, PlayerRole.DAMAGE, "Demonology Warlock"), 
                                                                                new Specialization(ClassList.WARLOCK, PlayerRole.DAMAGE, "Destruction Warlock") } },
            { (ClassList.WARRIOR, PlayerRole.TANK), new List <Specialization> {       new Specialization(ClassList.WARRIOR, PlayerRole.TANK, "Protection Warrior") } },
            { (ClassList.WARRIOR, PlayerRole.DAMAGE), new List <Specialization> {     new Specialization(ClassList.WARRIOR, PlayerRole.DAMAGE, "Arms Warrior"),
                                                                                new Specialization(ClassList.WARRIOR, PlayerRole.DAMAGE, "Fury Warrior") } },
            { (ClassList.NONE, PlayerRole.INVALID), new List <Specialization> {       new Specialization(ClassList.NONE, PlayerRole.INVALID, "Some Weirdness happening") } }

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
