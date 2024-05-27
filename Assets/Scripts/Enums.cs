using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AG
{
    public enum Race
    {
        None,
        Human,
        Dwarf,
        Elf,
        Orc,
        HumanLezard,
        HalfElf,
    }

    public enum PlayerClass
    {
        None,
        Warrior,
        Mage,
        Thief,
        Paladin,
        Rogue,
        Archer,
        Amazone,
        Berserk,
        Beggar,
    }

    public enum StartingObject
    {
        None,
        PieceOfBread,
        Key,
        HealPotion,
        Bomb,
        SealedLetter,
        Map,
        NoobRing,
        SellerBadge
    }

    public enum Modifier
    {
        None,
        HP,
        Damage,
        Chance,
        Perception,
        DamageAgainstDead,
        DamageAgainstLiving,
        Detection,
        DamageAgainstFlying,
        AllyStats,
        ImmuneToFemale,
        Critical,
        ShopPrice,
        MoreInfo,
        CriticChance,
        FailChance,
        Cost
    }

    public enum EquipmentType
    {
        None,
        Boot,
        Shield,
        Armor,
        Sword,
        Bow,
        Tunic,
        Staff,
        Hammer,
        Hat,
        Ring,
        Consummable,
        Coin,
        Helmet,
        Useless
    }

    public enum DoorDirection
    {
        None,
        BiDirectionnal,
        MonoDirectionnal,
    }

    public enum DungeonStateType
    {
        StartRoom,
        InRoom,
        EndRoom,
        Door,
        Combat,
        Event,
        TurnAround,
    }

    public enum CardType
    {
        None,
        GoLeft,
        GoRight,
        GoTop,
        GoBottom,
        EnterRoom,
        OpenDoor,
        RunAwayDoor,
        RunAwayRoom,
        TurnAround,
        Observe,
        Attack,
        Continue,
        Loot,
        NotLoot,
        SealedLetter,
        UseObject,
        Equipment,
    }

    public enum DoorMaterial
    {
        Wood,
        Metal,
        Gold,
    }

    public enum DoorType
    {
        None,
        Normal,
        Trapped,
        Barricaded,

        MAX
    }

    public enum RoomType
    {
        None,
        Entry,
        Empty,
        Event,
        Monster,
        Chest,

        MAX,
        Boss,
    }

    public enum EventType
    {
        LostElf,
        Dwarf,

        MAX
    }
}