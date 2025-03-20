using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

namespace UserApplication.Models

{

    public enum PRole
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
        public Specialization(ClassList Class, PRole role, string name)
        {
            Special = Class;
            Role = role;
            Name = name;
        }
        public ClassList Special { get; set; }
        public PRole Role { get; set; }
        public string Name { get; set; }
    }

    public static class ClassMappings
    {
        //defines the roles allowed Per ClassList
        public static readonly Dictionary<ClassList, List<PRole>> AllowedRoles = new()
        {
            { ClassList.DEATH_KNIGHT, new List<PRole> {PRole.TANK, PRole.DAMAGE } },
            { ClassList.DEMON_HUNTER, new List<PRole> {PRole.TANK, PRole.DAMAGE } },
            { ClassList.DRUID, new List<PRole> {PRole.TANK, PRole.HEALER, PRole.DAMAGE } },
            { ClassList.EVOKER, new List<PRole> { PRole.HEALER, PRole.DAMAGE } },
            { ClassList.HUNTER, new List<PRole> { PRole.DAMAGE } },
            { ClassList.MAGE, new List<PRole> {PRole.DAMAGE } },
            { ClassList.MONK, new List<PRole> {PRole.TANK, PRole.HEALER, PRole.DAMAGE } },
            { ClassList.PALADIN, new List<PRole> {PRole.TANK, PRole.HEALER, PRole.DAMAGE } },
            { ClassList.PRIEST, new List<PRole> {PRole.HEALER, PRole.DAMAGE } },
            { ClassList.ROGUE, new List<PRole> {PRole.DAMAGE } },
            { ClassList.SHAMAN, new List<PRole> {PRole.HEALER, PRole.DAMAGE } },
            { ClassList.WARLOCK, new List<PRole> {PRole.DAMAGE } },
            { ClassList.WARRIOR, new List<PRole> {PRole.TANK, PRole.DAMAGE } }
        };

        public static readonly Dictionary<(ClassList, PRole), List<Specialization>> Specializations = new()
        {
            { (ClassList.DEATH_KNIGHT, PRole.TANK), new List <Specialization> {  new Specialization(ClassList.DEATH_KNIGHT, PRole.TANK, "Blood Death Knight") } },
            { (ClassList.DEATH_KNIGHT, PRole.DAMAGE), new List <Specialization> {new Specialization(ClassList.DEATH_KNIGHT, PRole.DAMAGE, "Frost Death Knight" ), 
                                                                                new Specialization(ClassList.DEATH_KNIGHT, PRole.DAMAGE, "Unholy Death Knight") } },
            { (ClassList.DEMON_HUNTER, PRole.TANK), new List <Specialization> {  new Specialization(ClassList.DEMON_HUNTER, PRole.TANK, "Vengeance Demon Hunter") } },
            { (ClassList.DEMON_HUNTER, PRole.DAMAGE), new List <Specialization> {new Specialization(ClassList.DEMON_HUNTER, PRole.DAMAGE, "Havoc Demon Hunter") } },
            { (ClassList.DRUID, PRole.TANK), new List <Specialization> {         new Specialization(ClassList.DRUID, PRole.TANK, "Guardian Druid") } },
            { (ClassList.DRUID, PRole.HEALER),new List <Specialization> {        new Specialization(ClassList.DRUID, PRole.HEALER, "Restoration Druid") } },
            { (ClassList.DRUID, PRole.DAMAGE), new List <Specialization> {       new Specialization(ClassList.DEATH_KNIGHT, PRole.DAMAGE, "Feral Druid"), 
                                                                                new Specialization(ClassList.DRUID, PRole.DAMAGE, "Balance Druid") } },
            { (ClassList.EVOKER, PRole.HEALER), new List <Specialization>  {     new Specialization(ClassList.EVOKER, PRole.HEALER, "Presevation Evoker") } },
            { (ClassList.EVOKER, PRole.DAMAGE),new List <Specialization>  {      new Specialization(ClassList.EVOKER, PRole.HEALER, "Devestation Evoker"),
                                                                                new Specialization(ClassList.EVOKER, PRole.HEALER, "Augmentation Evoker") } },
            { (ClassList.HUNTER, PRole.DAMAGE), new List <Specialization> {      new Specialization(ClassList.HUNTER, PRole.DAMAGE, "Beast Master Hunter"), 
                                                                                new Specialization(ClassList.HUNTER, PRole.DAMAGE, "Marksmenship Hunter"),
                                                                                new Specialization(ClassList.HUNTER, PRole.DAMAGE, "Survival Hunter") } },
            { (ClassList.MAGE, PRole.DAMAGE), new List <Specialization> {        new Specialization(ClassList.MAGE, PRole.DAMAGE, "Fire Mage"), 
                                                                                new Specialization(ClassList.MAGE, PRole.DAMAGE, "Frost Mage"),   
                                                                                new Specialization(ClassList.MAGE, PRole.DAMAGE, "Arcane Mage") } },
            { (ClassList.MONK, PRole.TANK), new List <Specialization> {          new Specialization(ClassList.MONK, PRole.TANK, "Brewmaster Monk") } },
            { (ClassList.MONK, PRole.HEALER),new List <Specialization> {         new Specialization(ClassList.MONK, PRole.HEALER, "Mistweaver Monk") } },
            { (ClassList.MONK, PRole.DAMAGE), new List <Specialization> {        new Specialization(ClassList.MONK, PRole.DAMAGE, "Windwalker Monk") } },
            { (ClassList.PRIEST, PRole.HEALER), new List <Specialization>  {     new Specialization(ClassList.PRIEST, PRole.HEALER, "Holy Priest") , 
                                                                                new Specialization(ClassList.PRIEST, PRole.HEALER, "Disc Priest") } },
            { (ClassList.PRIEST, PRole.DAMAGE),new List <Specialization>  {      new Specialization(ClassList.PRIEST, PRole.DAMAGE, "Shadow Priest") } },
            { (ClassList.PALADIN, PRole.TANK), new List <Specialization> {       new Specialization(ClassList.PALADIN, PRole.TANK, "Protection Paladin") } },
            { (ClassList.PALADIN, PRole.HEALER),new List <Specialization>  {     new Specialization(ClassList.PALADIN, PRole.HEALER, "Holy Paladin") } },
            { (ClassList.PALADIN, PRole.DAMAGE),new List <Specialization> {      new Specialization(ClassList.PALADIN, PRole.DAMAGE, "Retribution Paladin") } },
            { (ClassList.ROGUE, PRole.DAMAGE), new List <Specialization> {       new Specialization(ClassList.ROGUE, PRole.DAMAGE, "Assassination Rogue"), 
                                                                                new Specialization(ClassList.ROGUE, PRole.DAMAGE, "Outlaw Rogue"), 
                                                                                new Specialization(ClassList.ROGUE, PRole.DAMAGE, "Subtlety Rogue") } },
            { (ClassList.SHAMAN, PRole.HEALER), new List <Specialization>  {     new Specialization(ClassList.SHAMAN, PRole.HEALER, "Restoration Shaman") } },
            { (ClassList.SHAMAN, PRole.DAMAGE),new List <Specialization>  {      new Specialization(ClassList.SHAMAN, PRole.DAMAGE, "Elemental Shaman"), 
                                                                                new Specialization(ClassList.SHAMAN, PRole.DAMAGE, "Enhancement Shaman") } },
            { (ClassList.WARLOCK, PRole.DAMAGE), new List <Specialization> {     new Specialization(ClassList.WARLOCK, PRole.DAMAGE, "Affliction Warlock"),
                                                                                new Specialization(ClassList.WARLOCK, PRole.DAMAGE, "Demonology Warlock"), 
                                                                                new Specialization(ClassList.WARLOCK, PRole.DAMAGE, "Destruction Warlock") } },
            { (ClassList.WARRIOR, PRole.TANK), new List <Specialization> {       new Specialization(ClassList.WARRIOR, PRole.TANK, "Protection Warrior") } },
            { (ClassList.WARRIOR, PRole.DAMAGE), new List <Specialization> {     new Specialization(ClassList.WARRIOR, PRole.DAMAGE, "Arms Warrior"),
                                                                                new Specialization(ClassList.WARRIOR, PRole.DAMAGE, "Fury Warrior") } },
            { (ClassList.NONE, PRole.INVALID), new List <Specialization> {       new Specialization(ClassList.NONE, PRole.INVALID, "Some Weirdness happening") } }

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
