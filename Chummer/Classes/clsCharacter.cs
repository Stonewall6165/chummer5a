﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

// MAGEnabledChanged Event Handler
public delegate void MAGEnabledChangedHandler(Object sender);
// RESEnabledChanged Event Handler
public delegate void RESEnabledChangedHandler(Object sender);
// AdeptTabEnabledChanged Event Handler
public delegate void AdeptTabEnabledChangedHandler(Object sender);
// MagicianTabEnabledChanged Event Handler
public delegate void MagicianTabEnabledChangedHandler(Object sender);
// TechnomancerTabEnabledChanged Event Handler
public delegate void TechnomancerTabEnabledChangedHandler(Object sender);
// InitiationTabEnabledChanged Event Handler
public delegate void InitiationTabEnabledChangedHandler(Object sender);
// CritterTabEnabledChanged Event Handler
public delegate void CritterTabEnabledChangedHandler(Object sender);
// UneducatedChanged Event Handler
public delegate void UneducatedChangedHandler(Object sender);
// JackOfAllTradesChanged Event Handler
public delegate void JackOfAllTradesChangedHandler(Object sender);
// CollegeEducationChanged Event Handler
public delegate void CollegeEducationChangedHandler(Object sender);
// SchoolOfHardKnocksChanged Event Handler
public delegate void SchoolOfHardKnocksChangedHandler(Object sender);
// UncouthChanged Event Handler
public delegate void UncouthChangedHandler(Object sender);
// InfirmChanged Event Handler
public delegate void InfirmChangedHandler(Object sender);
// FriendsInHighPlacesChanged Event Handler
public delegate void FriendsInHighPlacesChangedHandler(Object sender);
// CharacterNameChanged Event Handler
public delegate void CharacterNameChangedHandler(Object sender);
// BlackMarketEnabledChanged Event Handler
public delegate void BlackMarketEnabledChangedHandler(Object sender);
public delegate void ExConChangedHandler(Object sender);
public delegate void TrustFundChangedHandler(Object sender);
public delegate void TechSchoolChangedHandler(Object sender);
public delegate void RestrictedGearChangedHandler(Object sender);
public delegate void OverclockerChangedHandler(Object sender);
public delegate void MadeManChangedHandler(Object sender);
public delegate void LinguistChangedHandler(Object sender);
public delegate void LightningReflexesChangedHandler(Object sender);
public delegate void FameChangedHandler(Object sender);
public delegate void BornRichChangedHandler(Object sender);
public delegate void ErasedChangedHandler(Object sender);

namespace Chummer
{
    public enum CharacterBuildMethod
    {
        Karma = 0,
        Priority = 1,
        SumtoTen = 2,
        LifeModule = 3
    }

    /// <summary>
    /// Class that holds all of the information that makes up a complete Character.
    /// </summary>
    public class Character
    {
        private readonly ImprovementManager _objImprovementManager;
        private readonly CharacterOptions _objOptions = new CharacterOptions();

        private string _strFileName = "";
        private string _strSettingsFileName = "default.xml";
        private bool _blnIgnoreRules = false;
        private int _intKarma = 0;
        private int _intTotalKarma = 0;
        private int _intStreetCred = 0;
        private int _intNotoriety = 0;
        private int _intPublicAwareness = 0;
        private int _intBurntStreetCred = 0;
        private int _intNuyen = 0;
        private int _intStartingNuyen = 0;
        private int _intMaxAvail = 12;
        private decimal _decEssenceAtSpecialStart = 6.0m;
        private int _intSpecial = 0;
        private int _intTotalSpecial = 0;
        private int _intAttributes = 0;
        private int _intTotalAttributes = 0;
        private int _intSpellLimit = 0;
        private int _intCFPLimit = 0;
        private int _intContactPoints = 0;
        private int _intContactPointsUsed = 0;
        public int metageneticLimit = 0;

        // General character info.
        private string _strName = "";
        private string _strMugshot = "";
        private string _strSex = "";
        private string _strAge = "";
        private string _strEyes = "";
        private string _strHeight = "";
        private string _strWeight = "";
        private string _strSkin = "";
        private string _strHair = "";
        private string _strDescription = "";
        private string _strBackground = "";
        private string _strConcept = "";
        private string _strNotes = "";
        private string _strAlias = "";
        private string _strPlayerName = "";
        private string _strGameNotes = "";

        // If true, the Character creation has been finalized and is maintained through Karma.
        private bool _blnCreated = false;

        // Build Points
	    private int _intSumtoTen = 10;
        private int _intBuildPoints = 800;
        private int _intKnowledgeSkills = 0;
        private int _intKnowledgeSkillPoints = 0;
        private int _intSkillPoints = 0;
        private int _intSkillPointsMaximum = 0;
        private int _intSkillGroups = 0;
        private int _intSkillGroupsMaximum = 0;
        private decimal _decNuyenMaximumBP = 50m;
        private decimal _decNuyenBP = 0m;
        private int _intBuildKarma = 0;
        private int _intAdeptWayDiscount = 0;
	    private int _intGameplayOptionQualityLimit = 25;
        private CharacterBuildMethod _objBuildMethod = CharacterBuildMethod.Karma;

        // Metatype Information.
        private string _strMetatype = "";
        private string _strMetavariant = "";
        private string _strMetatypeCategory = "";
        private string _strMovement = "";
        private string _strWalk = "";
        private string _strRun = "";
        private string _strSprint = "";
        private int _intMetatypeBP = 0;
        private int _intMutantCritterBaseSkills = 0;

        // Special Flags.
        private bool _blnAdeptEnabled = false;
        private bool _blnMagicianEnabled = false;
        private bool _blnTechnomancerEnabled = false;
        private bool _blnInitiationEnabled = false;
        private bool _blnCritterEnabled = false;
        private bool _blnUneducated = false;
        private bool _blnUncouth = false;
        private bool _blnSchoolOfHardKnocks = false;
        private bool _blnInfirm = false;
        private bool _blnIsCritter = false;
        private bool _blnPossessed = false;
        private bool _blnBlackMarket = false;
        private bool _blnCollegeEducation = false;
        private bool _blnFriendsInHighPlaces = false;
        private bool _blnJackOfAllTrades = false;
        private bool _blnExCon = false;
        private bool _blnTrustFund = false;
        private bool _blnTechSchool = false;
        private bool _blnRestrictedGear = false;
        private bool _blnOverclocker = false;
        private bool _blnMadeMan = false;
        private bool _blnLinguist = false;
        private bool _blnLightningReflexes = false;
        private bool _blnFame = false;
        private bool _blnBornRich = false;
        private bool _blnBlackMarketPipeline = false;
        private bool _blnErased = false;

        // Attributes.
        private Attribute _attBOD = new Attribute("BOD");
        private Attribute _attAGI = new Attribute("AGI");
        private Attribute _attREA = new Attribute("REA");
        private Attribute _attSTR = new Attribute("STR");
        private Attribute _attCHA = new Attribute("CHA");
        private Attribute _attINT = new Attribute("INT");
        private Attribute _attLOG = new Attribute("LOG");
        private Attribute _attWIL = new Attribute("WIL");
        private Attribute _attINI = new Attribute("INI");
        private Attribute _attEDG = new Attribute("EDG");
        private Attribute _attMAG = new Attribute("MAG");
        private Attribute _attRES = new Attribute("RES");
        private Attribute _attESS = new Attribute("ESS");
        private bool _blnMAGEnabled = false;
        private bool _blnRESEnabled = false;
        private bool _blnGroupMember = false;
        private string _strGroupName = "";
        private string _strGroupNotes = "";
        private int _intInitiateGrade = 0;
        private int _intSubmersionGrade = 0;
        private int _intResponse = 1;
        private int _intSignal = 1;
        private int _intMaxSkillRating = 0;
        private bool _blnOverrideSpecialAttributeESSLoss = false;

        // Pseudo-Attributes use for Mystic Adepts.
        private int _intMAGMagician = 0;
        private int _intMAGAdept = 0;

        // Magic Tradition.
        private string _strMagicTradition = "";
        private string _strTraditionDrain = "";
        private string _strTraditionName = "";
        private string _strSpiritCombat = "";
        private string _strSpiritDetection = "";
        private string _strSpiritHealth = "";
        private string _strSpiritIllusion = "";
        private string _strSpiritManipulation = "";
        // Technomancer Stream.
        private string _strTechnomancerStream = "Default";

        // Condition Monitor Progress.
        private int _intPhysicalCMFilled = 0;
        private int _intStunCMFilled = 0;

        // Priority Selections.
        private string _strGameplayOption = "";
        private string _strPriorityMetatype = "";
        private string _strPriorityAttributes = "";
        private string _strPrioritySpecial = "";
        private string _strPrioritySkills = "";
        private string _strPriorityResources = "";
        private string _strSkill1 = "";
        private string _strSkill2 = "";
        private string _strSkillGroup = "";
        private int _intMaxNuyen = 0;
		private int _intMaxKarma = 0;
        private int _intContactMultiplier = 0;

        // Lists.
        private List<Improvement> _lstImprovements = new List<Improvement>();
        private List<Skill> _lstSkills = new List<Skill>();
        private List<SkillGroup> _lstSkillGroups = new List<SkillGroup>();
        private List<Contact> _lstContacts = new List<Contact>();
        private List<Spirit> _lstSpirits = new List<Spirit>();
        private List<Spell> _lstSpells = new List<Spell>();
        private List<Focus> _lstFoci = new List<Focus>();
        private List<StackedFocus> _lstStackedFoci = new List<StackedFocus>();
        private List<Power> _lstPowers = new List<Power>();
        private List<ComplexForm> _lstComplexForms = new List<ComplexForm>();
        private List<MartialArt> _lstMartialArts = new List<MartialArt>();
        private List<MartialArtManeuver> _lstMartialArtManeuvers = new List<MartialArtManeuver>();
        private List<LimitModifier> _lstLimitModifiers = new List<LimitModifier>();
        private List<Armor> _lstArmor = new List<Armor>();
        private List<Cyberware> _lstCyberware = new List<Cyberware>();
        private List<Weapon> _lstWeapons = new List<Weapon>();
        private List<Quality> _lstQualities = new List<Quality>();
        private List<LifestyleQuality> _lstLifestyleQualities = new List<LifestyleQuality>();
        private List<Lifestyle> _lstLifestyles = new List<Lifestyle>();
        private List<Gear> _lstGear = new List<Gear>();
        private List<Vehicle> _lstVehicles = new List<Vehicle>();
        private List<Metamagic> _lstMetamagics = new List<Metamagic>();
        private List<Art> _lstArts = new List<Art>();
        private List<Enhancement> _lstEnhancements = new List<Enhancement>();
        private List<ExpenseLogEntry> _lstExpenseLog = new List<ExpenseLogEntry>();
        private List<CritterPower> _lstCritterPowers = new List<CritterPower>();
        private List<InitiationGrade> _lstInitiationGrades = new List<InitiationGrade>();
        private List<string> _lstOldQualities = new List<string>();
        private List<string> _lstLocations = new List<string>();
        private List<string> _lstArmorBundles = new List<string>();
        private List<string> _lstWeaponLocations = new List<string>();
        private List<string> _lstImprovementGroups = new List<string>();
        private List<CalendarWeek> _lstCalendar = new List<CalendarWeek>();
        //private List<LifeModule> _lstLifeModules = new List<LifeModule>();

        // Character Version
        private string _strVersionCreated = Application.ProductVersion.ToString().Replace("0.0.", string.Empty);

		// Events.
		public event AdeptTabEnabledChangedHandler AdeptTabEnabledChanged;
		public event CollegeEducationChangedHandler CollegeEducationChanged;
		public event CritterTabEnabledChangedHandler CritterTabEnabledChanged;
		public event MAGEnabledChangedHandler MAGEnabledChanged;
		public event BlackMarketEnabledChangedHandler BlackMarketEnabledChanged;
		public event BornRichChangedHandler BornRichChanged;
		public event CharacterNameChangedHandler CharacterNameChanged;
		public event ErasedChangedHandler ErasedChanged;
		public event ExConChangedHandler ExConChanged;
		public event FameChangedHandler FameChanged;
		public event FriendsInHighPlacesChangedHandler FriendsInHighPlacesChanged;
		public event InfirmChangedHandler InfirmChanged;
		public event InitiationTabEnabledChangedHandler InitiationTabEnabledChanged;
		public event JackOfAllTradesChangedHandler JackOfAllTradesChanged;
		public event LightningReflexesChangedHandler LightningReflexesChanged;
		public event LinguistChangedHandler LinguistChanged;
		public event MadeManChangedHandler MadeManChanged;
		public event MagicianTabEnabledChangedHandler MagicianTabEnabledChanged;
		public event OverclockerChangedHandler OverclockerChanged;
		public event RESEnabledChangedHandler RESEnabledChanged;
		public event RestrictedGearChangedHandler RestrictedGearChanged;
		public event SchoolOfHardKnocksChangedHandler SchoolOfHardKnocksChanged;
		public event TechnomancerTabEnabledChangedHandler TechnomancerTabEnabledChanged;
		public event TechSchoolChangedHandler TechSchoolChanged;
		public event TrustFundChangedHandler TrustFundChanged;
		public event UncouthChangedHandler UncouthChanged;
		public event UneducatedChangedHandler UneducatedChanged;

		private frmViewer _frmPrintView;

        #region Initialization, Save, Load, Print, and Reset Methods
        /// <summary>
        /// Character.
        /// </summary>
        public Character()
        {
            _attBOD._objCharacter = this;
            _attAGI._objCharacter = this;
            _attREA._objCharacter = this;
            _attSTR._objCharacter = this;
            _attCHA._objCharacter = this;
            _attINT._objCharacter = this;
            _attLOG._objCharacter = this;
            _attWIL._objCharacter = this;
            _attINI._objCharacter = this;
            _attEDG._objCharacter = this;
            _attMAG._objCharacter = this;
            _attRES._objCharacter = this;
            _attESS._objCharacter = this;
            _objImprovementManager = new ImprovementManager(this);
        }

        /// <summary>
        /// Save the Character to an XML file.
        /// </summary>
        public void Save()
        {
            FileStream objStream = new FileStream(_strFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            XmlTextWriter objWriter = new XmlTextWriter(objStream, Encoding.Unicode);
            objWriter.Formatting = Formatting.Indented;
            objWriter.Indentation = 1;
            objWriter.IndentChar = '\t';

            objWriter.WriteStartDocument();

            // <character>
            objWriter.WriteStartElement("character");

            // <createdversion />
            objWriter.WriteElementString("createdversion", _strVersionCreated);
            // <appversion />
            objWriter.WriteElementString("appversion", Application.ProductVersion.ToString().Replace("0.0.", string.Empty));
            // <gameedition />
            objWriter.WriteElementString("gameedition", "SR5");

            // <settings />
            objWriter.WriteElementString("settings", _strSettingsFileName);

            // <metatype />
            objWriter.WriteElementString("metatype", _strMetatype);
            // <metatypebp />
            objWriter.WriteElementString("metatypebp", _intMetatypeBP.ToString());
            // <metavariant />
            objWriter.WriteElementString("metavariant", _strMetavariant);
            // <metatypecategory />
            objWriter.WriteElementString("metatypecategory", _strMetatypeCategory);
            // <movement />
            objWriter.WriteElementString("movement", _strMovement);
            // <walk />
            objWriter.WriteElementString("walk", _strWalk);
            // <run />
            objWriter.WriteElementString("run", _strRun);
            // <sprint />
            objWriter.WriteElementString("sprint", _strSprint);
            // <mutantcritterbaseskills />
            objWriter.WriteElementString("mutantcritterbaseskills", _intMutantCritterBaseSkills.ToString());

            // <prioritymetatype />
            objWriter.WriteElementString("prioritymetatype", _strPriorityMetatype);
            // <priorityattributes />
            objWriter.WriteElementString("priorityattributes", _strPriorityAttributes);
            // <priorityspecial />
            objWriter.WriteElementString("priorityspecial", _strPrioritySpecial);
            // <priorityskills />
            objWriter.WriteElementString("priorityskills", _strPrioritySkills);
            // <priorityresources />
            objWriter.WriteElementString("priorityresources", _strPriorityResources);
            // <priorityskill1 />
            objWriter.WriteElementString("priorityskill1", _strSkill1);
            // <priorityskill2 />
            objWriter.WriteElementString("priorityskill2", _strSkill2);
            // <priorityskillgroup />
            objWriter.WriteElementString("priorityskillgroup", _strSkillGroup);

            // <essenceatspecialstart />
            objWriter.WriteElementString("essenceatspecialstart", _decEssenceAtSpecialStart.ToString(GlobalOptions.Instance.CultureInfo));

            // <name />
            objWriter.WriteElementString("name", _strName);
            // <mugshot />
            objWriter.WriteElementString("mugshot", _strMugshot);
            // <sex />
            objWriter.WriteElementString("sex", _strSex);
            // <age />
            objWriter.WriteElementString("age", _strAge);
            // <eyes />
            objWriter.WriteElementString("eyes", _strEyes);
            // <height />
            objWriter.WriteElementString("height", _strHeight);
            // <weight />
            objWriter.WriteElementString("weight", _strWeight);
            // <skin />
            objWriter.WriteElementString("skin", _strSkin);
            // <hair />
            objWriter.WriteElementString("hair", _strHair);
            // <description />
            objWriter.WriteElementString("description", _strDescription);
            // <background />
            objWriter.WriteElementString("background", _strBackground);
            // <concept />
            objWriter.WriteElementString("concept", _strConcept);
            // <notes />
            objWriter.WriteElementString("notes", _strNotes);
            // <alias />
            objWriter.WriteElementString("alias", _strAlias);
            // <playername />
            objWriter.WriteElementString("playername", _strPlayerName);
            // <gamenotes />
            objWriter.WriteElementString("gamenotes", _strGameNotes);

            // <ignorerules />
            if (_blnIgnoreRules)
                objWriter.WriteElementString("ignorerules", _blnIgnoreRules.ToString());
            // <iscritter />
            if (_blnIsCritter)
                objWriter.WriteElementString("iscritter", _blnIsCritter.ToString());
            if (_blnPossessed)
                objWriter.WriteElementString("possessed", _blnPossessed.ToString());
            if (_blnOverrideSpecialAttributeESSLoss)
                objWriter.WriteElementString("overridespecialattributeessloss", _blnOverrideSpecialAttributeESSLoss.ToString());

            // <karma />
            objWriter.WriteElementString("karma", _intKarma.ToString());
            // <totalkarma />
            objWriter.WriteElementString("totalkarma", _intTotalKarma.ToString());
            // <special />
            objWriter.WriteElementString("special", _intSpecial.ToString());
            // <totalspecial />
            objWriter.WriteElementString("totalspecial", _intTotalSpecial.ToString());
            // <totalattributes />
            objWriter.WriteElementString("totalattributes", _intTotalAttributes.ToString());
            // <contactpoints />
            objWriter.WriteElementString("contactpoints", _intContactPoints.ToString());
            // <contactpoints />
            objWriter.WriteElementString("contactpointsused", _intContactPointsUsed.ToString());
            // <spelllimit />
            objWriter.WriteElementString("spelllimit", _intSpellLimit.ToString());
            // <cfplimit />
            objWriter.WriteElementString("cfplimit", _intCFPLimit.ToString());
            // <streetcred />
            objWriter.WriteElementString("streetcred", _intStreetCred.ToString());
            // <notoriety />
            objWriter.WriteElementString("notoriety", _intNotoriety.ToString());
            // <publicaware />
            objWriter.WriteElementString("publicawareness", _intPublicAwareness.ToString());
            // <burntstreetcred />
            objWriter.WriteElementString("burntstreetcred", _intBurntStreetCred.ToString());
            // <created />
            objWriter.WriteElementString("created", _blnCreated.ToString());
            // <maxavail />
            objWriter.WriteElementString("maxavail", _intMaxAvail.ToString());
            // <nuyen />
            objWriter.WriteElementString("nuyen", _intNuyen.ToString());
            // <nuyen />
            objWriter.WriteElementString("startingnuyen", _intStartingNuyen.ToString());
            // <adeptwaydiscount />
            objWriter.WriteElementString("adeptwaydiscount", _intAdeptWayDiscount.ToString());
			// <sumtoten />
			objWriter.WriteElementString("sumtoten", _intSumtoTen.ToString());
			// <buildpoints />
			objWriter.WriteElementString("bp", _intBuildPoints.ToString());
            // <buildkarma />
            objWriter.WriteElementString("buildkarma", _intBuildKarma.ToString());
            // <buildmethod />
            objWriter.WriteElementString("buildmethod", _objBuildMethod.ToString());
            // <gameplayoption />
            objWriter.WriteElementString("gameplayoption", _strGameplayOption);
			// <gameplayoptionqualitylimit />
			objWriter.WriteElementString("gameplayoptionqualitylimit", _intGameplayOptionQualityLimit.ToString());
			// <maxnuyen />
			objWriter.WriteElementString("maxnuyen", _intMaxNuyen.ToString());
            // <maxkarma />
            objWriter.WriteElementString("maxkarma", _intMaxKarma.ToString());
            // <contactmultiplier />
            objWriter.WriteElementString("contactmultiplier", _intContactMultiplier.ToString());

            // <knowpts />
            objWriter.WriteElementString("knowskillpts", _intKnowledgeSkills.ToString());
            // <knowpts />
            objWriter.WriteElementString("knowpts", _intKnowledgeSkillPoints.ToString());
            // <skillpts />
            objWriter.WriteElementString("skillpts", _intSkillPoints.ToString());
            // <skillptsmax />
            objWriter.WriteElementString("skillptsmax", _intSkillPointsMaximum.ToString());
            // <skillgrps />
            objWriter.WriteElementString("skillgrps", _intSkillGroups.ToString());
            // <skillgrpsmax />
            objWriter.WriteElementString("skillgrpsmax", _intSkillGroupsMaximum.ToString());

            // <nuyenbp />
            objWriter.WriteElementString("nuyenbp", _decNuyenBP.ToString());
            // <nuyenmaxbp />
            objWriter.WriteElementString("nuyenmaxbp", _decNuyenMaximumBP.ToString());

            // <adept />
            objWriter.WriteElementString("adept", _blnAdeptEnabled.ToString());
            // <magician />
            objWriter.WriteElementString("magician", _blnMagicianEnabled.ToString());
            // <technomancer />
            objWriter.WriteElementString("technomancer", _blnTechnomancerEnabled.ToString());
            // <initiationoverride />
            objWriter.WriteElementString("initiationoverride", _blnInitiationEnabled.ToString());
            // <critter />
            objWriter.WriteElementString("critter", _blnCritterEnabled.ToString());
            // <uneducated />
            objWriter.WriteElementString("uneducated", _blnUneducated.ToString());
            // <uncouth />
            objWriter.WriteElementString("uncouth", _blnUncouth.ToString());
            // <uncouth />
            objWriter.WriteElementString("schoolofhardknocks", _blnSchoolOfHardKnocks.ToString());
            // <collegeeducation />
            objWriter.WriteElementString("collegeeducation", _blnCollegeEducation.ToString());
            // <friendsinhighplaces />
            objWriter.WriteElementString("friendsinhighplaces", _blnFriendsInHighPlaces.ToString());
            // <jackofalltrades />
            objWriter.WriteElementString("jackofalltrades", _blnJackOfAllTrades.ToString());
            // <infirm />
            objWriter.WriteElementString("infirm", _blnInfirm.ToString());
            // <blackmarket />
            objWriter.WriteElementString("blackmarket", _blnBlackMarket.ToString());

            objWriter.WriteElementString("excon", _blnExCon.ToString());

            objWriter.WriteElementString("trustfund", _blnTrustFund.ToString());

            objWriter.WriteElementString("techschool", _blnTechSchool.ToString());

            objWriter.WriteElementString("restrictedgear", _blnRestrictedGear.ToString());

            objWriter.WriteElementString("overclocker", _blnOverclocker.ToString());

            objWriter.WriteElementString("mademan", _blnMadeMan.ToString());

            objWriter.WriteElementString("linguist", _blnLinguist.ToString());

            objWriter.WriteElementString("lightningreflexes", _blnLightningReflexes.ToString());

            objWriter.WriteElementString("fame", _blnFame.ToString());

            objWriter.WriteElementString("bornrich", _blnBornRich.ToString());

            objWriter.WriteElementString("erased", _blnErased.ToString());

            // <attributes>
            objWriter.WriteStartElement("attributes");
            _attBOD.Save(objWriter);
            _attAGI.Save(objWriter);
            _attREA.Save(objWriter);
            _attSTR.Save(objWriter);
            _attCHA.Save(objWriter);
            _attINT.Save(objWriter);
            _attLOG.Save(objWriter);
            _attWIL.Save(objWriter);
            _attINI.Save(objWriter);
            _attEDG.Save(objWriter);
            _attMAG.Save(objWriter);
            _attRES.Save(objWriter);
            _attESS.Save(objWriter);
            // Include any special A.I. Attributes if applicable.
            if (_strMetatype.EndsWith("A.I.") || _strMetatypeCategory == "Technocritters" || _strMetatypeCategory == "Protosapients")
            {
                objWriter.WriteElementString("response", _intResponse.ToString());
                objWriter.WriteElementString("signal", _intSignal.ToString());
            }
            if (_intMaxSkillRating > 0)
                objWriter.WriteElementString("maxskillrating", _intMaxSkillRating.ToString());
            // </attributes>
            objWriter.WriteEndElement();

            // <magenabled />
            objWriter.WriteElementString("magenabled", _blnMAGEnabled.ToString());
            // <initiategrade />
            objWriter.WriteElementString("initiategrade", _intInitiateGrade.ToString());
            // <resenabled />
            objWriter.WriteElementString("resenabled", _blnRESEnabled.ToString());
            // <submersiongrade />
            objWriter.WriteElementString("submersiongrade", _intSubmersionGrade.ToString());
            // <groupmember />
            objWriter.WriteElementString("groupmember", _blnGroupMember.ToString());
            // <groupname />
            objWriter.WriteElementString("groupname", _strGroupName);
            // <groupnotes />
            objWriter.WriteElementString("groupnotes", _strGroupNotes);

            // External reader friendly stuff.
            objWriter.WriteElementString("totaless", Essence.ToString());

            // Write out the Mystic Adept MAG split info.
            if (_blnAdeptEnabled && _blnMagicianEnabled)
            {
                objWriter.WriteElementString("magsplitadept", _intMAGAdept.ToString());
                objWriter.WriteElementString("magsplitmagician", _intMAGMagician.ToString());
            }

            // Write the Magic Tradition.
            objWriter.WriteElementString("tradition", _strMagicTradition);
            // Write the Drain Attributes.
            objWriter.WriteElementString("traditiondrain", _strTraditionDrain);
            // Write the Tradition Name.
            objWriter.WriteElementString("traditionname", _strTraditionName);
            // Write the Tradition Spirits.
            objWriter.WriteElementString("spiritcombat", _strSpiritCombat);
            objWriter.WriteElementString("spiritdetection", _strSpiritDetection);
            objWriter.WriteElementString("spirithealth", _strSpiritHealth);
            objWriter.WriteElementString("spiritillusion", _strSpiritIllusion);
            objWriter.WriteElementString("spiritmanipulation", _strSpiritManipulation);
            // Write the Technomancer Stream.
            objWriter.WriteElementString("stream", _strTechnomancerStream);

            // Condition Monitor Progress.
            // <physicalcmfilled />
            objWriter.WriteElementString("physicalcmfilled", _intPhysicalCMFilled.ToString());
            // <stuncmfilled />
            objWriter.WriteElementString("stuncmfilled", _intStunCMFilled.ToString());

            // <skillgroups>
            objWriter.WriteStartElement("skillgroups");
            foreach (SkillGroup objSkillGroup in _lstSkillGroups)
            {
                objSkillGroup.Save(objWriter);
            }
            // </skillgroups>
            objWriter.WriteEndElement();

            // <skills>
            objWriter.WriteStartElement("skills");
            foreach (Skill objSkill in _lstSkills)
            {
                objSkill.Save(objWriter);
            }
            // </skills>
            objWriter.WriteEndElement();

            // <contacts>
            objWriter.WriteStartElement("contacts");
            foreach (Contact objContact in _lstContacts)
            {
                objContact.Save(objWriter);
            }
            // </contacts>
            objWriter.WriteEndElement();

            // <spells>
            objWriter.WriteStartElement("spells");
            foreach (Spell objSpell in _lstSpells)
            {
                objSpell.Save(objWriter);
            }
            // </spells>
            objWriter.WriteEndElement();

            // <foci>
            objWriter.WriteStartElement("foci");
            foreach (Focus objFocus in _lstFoci)
            {
                objFocus.Save(objWriter);
            }
            // </foci>
            objWriter.WriteEndElement();

            // <stackedfoci>
            objWriter.WriteStartElement("stackedfoci");
            foreach (StackedFocus objStack in _lstStackedFoci)
            {
                objStack.Save(objWriter);
            }
            // </stackedfoci>
            objWriter.WriteEndElement();

            // <powers>
            objWriter.WriteStartElement("powers");
            foreach (Power objPower in _lstPowers)
            {
                objPower.Save(objWriter);
            }
            // </powers>
            objWriter.WriteEndElement();

            // <spirits>
            objWriter.WriteStartElement("spirits");
            foreach (Spirit objSpirit in _lstSpirits)
            {
                objSpirit.Save(objWriter);
            }
            // </spirits>
            objWriter.WriteEndElement();

            // <complexforms>
            objWriter.WriteStartElement("complexforms");
            foreach (ComplexForm objProgram in _lstComplexForms)
            {
                objProgram.Save(objWriter);
            }
            // </complexforms>
            objWriter.WriteEndElement();

            // <martialarts>
            objWriter.WriteStartElement("martialarts");
            foreach (MartialArt objMartialArt in _lstMartialArts)
            {
                objMartialArt.Save(objWriter);
            }
            // </martialarts>
            objWriter.WriteEndElement();

            // <martialartmaneuvers>
            objWriter.WriteStartElement("martialartmaneuvers");
            foreach (MartialArtManeuver objManeuver in _lstMartialArtManeuvers)
            {
                objManeuver.Save(objWriter);
            }
            // </martialartmaneuvers>
            objWriter.WriteEndElement();

            // <limitmodifiers>
            objWriter.WriteStartElement("limitmodifiers");
            foreach (LimitModifier objLimitModifier in _lstLimitModifiers)
            {
                objLimitModifier.Save(objWriter);
            }
            // </limitmodifiers>
            objWriter.WriteEndElement();

            // <armors>
            objWriter.WriteStartElement("armors");
            foreach (Armor objArmor in _lstArmor)
            {
                objArmor.Save(objWriter);
            }
            // </armors>
            objWriter.WriteEndElement();

            // <weapons>
            objWriter.WriteStartElement("weapons");
            foreach (Weapon objWeapon in _lstWeapons)
            {
                objWeapon.Save(objWriter);
            }
            // </weapons>
            objWriter.WriteEndElement();

            // <cyberwares>
            objWriter.WriteStartElement("cyberwares");
            foreach (Cyberware objCyberware in _lstCyberware)
            {
                objCyberware.Save(objWriter);
            }
            // </cyberwares>
            objWriter.WriteEndElement();

            // <qualities>
            objWriter.WriteStartElement("qualities");
            foreach (Quality objQuality in _lstQualities)
            {
                objQuality.Save(objWriter);
            }
            // </qualities>
            objWriter.WriteEndElement();

            // <lifestyles>
            objWriter.WriteStartElement("lifestyles");
            foreach (Lifestyle objLifestyle in _lstLifestyles)
            {
                objLifestyle.Save(objWriter);
            }
            // </lifestyles>
            objWriter.WriteEndElement();

            // <gears>
            objWriter.WriteStartElement("gears");
            foreach (Gear objGear in _lstGear)
            {
                // Use the Gear's SubClass if applicable.
                if (objGear.GetType() == typeof(Commlink))
                {
                    Commlink objCommlink = new Commlink(this);
                    objCommlink = (Commlink)objGear;
                    objCommlink.Save(objWriter);
                }
                else
                {
                    objGear.Save(objWriter);
                }
            }
            // </gears>
            objWriter.WriteEndElement();

            // <vehicles>
            objWriter.WriteStartElement("vehicles");
            foreach (Vehicle objVehicle in _lstVehicles)
            {
                objVehicle.Save(objWriter);
            }
            // </vehicles>
            objWriter.WriteEndElement();

            // <metamagics>
            objWriter.WriteStartElement("metamagics");
            foreach (Metamagic objMetamagic in _lstMetamagics)
            {
                objMetamagic.Save(objWriter);
            }
            // </metamagics>
            objWriter.WriteEndElement();

            // <arts>
            objWriter.WriteStartElement("arts");
            foreach (Art objArt in _lstArts)
            {
                objArt.Save(objWriter);
            }
            // </arts>
            objWriter.WriteEndElement();

            // <enhancements>
            objWriter.WriteStartElement("enhancements");
            foreach (Enhancement objEnhancement in _lstEnhancements)
            {
                objEnhancement.Save(objWriter);
            }
            // </enhancements>
            objWriter.WriteEndElement();

            // <critterpowers>
            objWriter.WriteStartElement("critterpowers");
            foreach (CritterPower objPower in _lstCritterPowers)
            {
                objPower.Save(objWriter);
            }
            // </critterpowers>
            objWriter.WriteEndElement();

            // <initiationgrades>
            objWriter.WriteStartElement("initiationgrades");
            foreach (InitiationGrade objGrade in _lstInitiationGrades)
            {
                objGrade.Save(objWriter);
            }
            // </initiationgrades>
            objWriter.WriteEndElement();

            // <improvements>
            objWriter.WriteStartElement("improvements");
            foreach (Improvement objImprovement in _lstImprovements)
            {
                objImprovement.Save(objWriter);
            }
            // </improvements>
            objWriter.WriteEndElement();

            // <expenses>
            objWriter.WriteStartElement("expenses");
            foreach (ExpenseLogEntry objExpenseLogEntry in _lstExpenseLog)
            {
                objExpenseLogEntry.Save(objWriter);
            }
            // </expenses>
            objWriter.WriteEndElement();

            // <locations>
            objWriter.WriteStartElement("locations");
            foreach (string strLocation in _lstLocations)
            {
                objWriter.WriteElementString("location", strLocation);
            }
            // </locations>
            objWriter.WriteEndElement();

            // <armorbundles>
            objWriter.WriteStartElement("armorbundles");
            foreach (string strBundle in _lstArmorBundles)
            {
                objWriter.WriteElementString("armorbundle", strBundle);
            }
            // </armorbundles>
            objWriter.WriteEndElement();

            // <weaponlocations>
            objWriter.WriteStartElement("weaponlocations");
            foreach (string strLocation in _lstWeaponLocations)
            {
                objWriter.WriteElementString("weaponlocation", strLocation);
            }
            // </weaponlocations>
            objWriter.WriteEndElement();

            // <improvementgroups>
            objWriter.WriteStartElement("improvementgroups");
            foreach (string strGroup in _lstImprovementGroups)
            {
                objWriter.WriteElementString("improvementgroup", strGroup);
            }
            // </improvementgroups>
            objWriter.WriteEndElement();

            // <calendar>
            objWriter.WriteStartElement("calendar");
            foreach (CalendarWeek objWeek in _lstCalendar)
            {
                objWeek.Save(objWriter);
            }
            objWriter.WriteEndElement();
            // </calendar>

            // </character>
            objWriter.WriteEndElement();

            objWriter.WriteEndDocument();
            objWriter.Close();
            objStream.Close();
        }

        /// <summary>
        /// Load the Character from an XML file.
        /// </summary>
        public bool Load()
        {

            XmlDocument objXmlDocument = new XmlDocument();
            objXmlDocument.Load(_strFileName);

            XmlNode objXmlCharacter = objXmlDocument.SelectSingleNode("/character");
            XmlNodeList objXmlNodeList;

            try
            {
                _blnIgnoreRules = Convert.ToBoolean(objXmlCharacter["ignorerules"].InnerText);
            }
            catch
            {
                _blnIgnoreRules = false;
            }
            try
            {
                _blnCreated = Convert.ToBoolean(objXmlCharacter["created"].InnerText);
            }
            catch
            {
            }

            ResetCharacter();

            // Get the game edition of the file if possible and make sure it's intended to be used with this version of the application.
            try
            {
                if (objXmlCharacter["gameedition"].InnerText != string.Empty && objXmlCharacter["gameedition"].InnerText != "SR5")
                {
                    MessageBox.Show(LanguageManager.Instance.GetString("Message_IncorrectGameVersion_SR4"), LanguageManager.Instance.GetString("MessageTitle_IncorrectGameVersion"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
            }

            // Get the name of the settings file in use if possible.
            try
            {
                _strSettingsFileName = objXmlCharacter["settings"].InnerText;
            }
            catch
            {
            }

            // Load the character's settings file.
            if (!_objOptions.Load(_strSettingsFileName))
                return false;

            try
            {
                _decEssenceAtSpecialStart = Convert.ToDecimal(objXmlCharacter["essenceatspecialstart"].InnerText, GlobalOptions.Instance.CultureInfo);
                // fix to work around a mistake made when saving decimal values in previous versions.
                if (_decEssenceAtSpecialStart > EssenceMaximum)
                    _decEssenceAtSpecialStart /= 10;
            }
            catch
            {
            }

            try
            {
                _strVersionCreated = objXmlCharacter["createdversion"].InnerText;
            }
            catch
            {
            }

            // Metatype information.
            _strMetatype = objXmlCharacter["metatype"].InnerText;
            try
            {
                _strWalk = objXmlCharacter["walk"].InnerText;
                _strRun = objXmlCharacter["run"].InnerText;
                _strSprint = objXmlCharacter["sprint"].InnerText;
            }
            catch
            {
            }
            _intMetatypeBP = Convert.ToInt32(objXmlCharacter["metatypebp"].InnerText);
            _strMetavariant = objXmlCharacter["metavariant"].InnerText;
            try
            {
                _strMetatypeCategory = objXmlCharacter["metatypecategory"].InnerText;
            }
            catch
            {
            }
            try
            {
                _intMutantCritterBaseSkills = Convert.ToInt32(objXmlCharacter["mutantcritterbaseskills"].InnerText);
            }
            catch
            {
            }

            // General character information.
            _strName = objXmlCharacter["name"].InnerText;
            try
            {
                _strMugshot = objXmlCharacter["mugshot"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strSex = objXmlCharacter["sex"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strAge = objXmlCharacter["age"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strEyes = objXmlCharacter["eyes"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strHeight = objXmlCharacter["height"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strWeight = objXmlCharacter["weight"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strSkin = objXmlCharacter["skin"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strHair = objXmlCharacter["hair"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strDescription = objXmlCharacter["description"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strBackground = objXmlCharacter["background"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strConcept = objXmlCharacter["concept"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strNotes = objXmlCharacter["notes"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strAlias = objXmlCharacter["alias"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strPlayerName = objXmlCharacter["playername"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strGameNotes = objXmlCharacter["gamenotes"].InnerText;
            }
            catch
            {
            }

            try
            {
                _strGameplayOption = objXmlCharacter["gameplayoption"].InnerText;
            }
            catch
            {
            }
            try
            {
                _intMaxNuyen = Convert.ToInt32(objXmlCharacter["maxnuyen"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intContactMultiplier = Convert.ToInt32(objXmlCharacter["contactmultiplier"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intMaxKarma = Convert.ToInt32(objXmlCharacter["maxkarma"].InnerText);
            }
            catch
            {
            }
            try
            {
                _strPriorityMetatype = objXmlCharacter["prioritymetatype"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strPriorityAttributes = objXmlCharacter["priorityattributes"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strPrioritySpecial = objXmlCharacter["priorityspecial"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strPrioritySkills = objXmlCharacter["priorityskills"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strPriorityResources = objXmlCharacter["priorityresources"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strSkill1 = objXmlCharacter["priorityskill1"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strSkill2 = objXmlCharacter["priorityskill2"].InnerText;
            }
            catch
            {
            }
            try
            {
                _strSkillGroup = objXmlCharacter["priorityskillgroup"].InnerText;
            }
            catch
            {
            }

            try
            {
                _blnIsCritter = Convert.ToBoolean(objXmlCharacter["iscritter"].InnerText);
            }
            catch
            {
            }

            try
            {
                _blnPossessed = Convert.ToBoolean(objXmlCharacter["possessed"].InnerText);
            }
            catch
            {
            }

            try
            {
                _blnOverrideSpecialAttributeESSLoss = Convert.ToBoolean(objXmlCharacter["overridespecialattributeessloss"].InnerText);
            }
            catch
            {
            }

            try
            {
                _intContactPoints = Convert.ToInt32(objXmlCharacter["contactpoints"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intContactPointsUsed = Convert.ToInt32(objXmlCharacter["contactpointsused"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intCFPLimit = Convert.ToInt32(objXmlCharacter["cfplimit"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intSpellLimit = Convert.ToInt32(objXmlCharacter["spelllimit"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intKarma = Convert.ToInt32(objXmlCharacter["karma"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intTotalKarma = Convert.ToInt32(objXmlCharacter["totalkarma"].InnerText);
            }
            catch
            {
            }

            try
            {
                _intSpecial = Convert.ToInt32(objXmlCharacter["special"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intTotalSpecial = Convert.ToInt32(objXmlCharacter["totalspecial"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intAttributes = Convert.ToInt32(objXmlCharacter["attributes"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intTotalAttributes = Convert.ToInt32(objXmlCharacter["totalattributes"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intContactPoints = Convert.ToInt32(objXmlCharacter["contactpoints"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intContactPointsUsed = Convert.ToInt32(objXmlCharacter["contactpointsused"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intStreetCred = Convert.ToInt32(objXmlCharacter["streetcred"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intNotoriety = Convert.ToInt32(objXmlCharacter["notoriety"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intPublicAwareness = Convert.ToInt32(objXmlCharacter["publicawareness"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intBurntStreetCred = Convert.ToInt32(objXmlCharacter["burntstreetcred"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intMaxAvail = Convert.ToInt32(objXmlCharacter["maxavail"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intNuyen = Convert.ToInt32(objXmlCharacter["nuyen"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intStartingNuyen = Convert.ToInt32(objXmlCharacter["startingnuyen"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intAdeptWayDiscount = Convert.ToInt32(objXmlCharacter["adeptwaydiscount"].InnerText);
            }
            catch
            {
            }

			// Sum to X point value.
			try
			{
				_intSumtoTen = Convert.ToInt32(objXmlCharacter["sumtoten"].InnerText);
			}
			catch
			{
			}
			// Build Points/Karma.
			_intBuildPoints = Convert.ToInt32(objXmlCharacter["bp"].InnerText);
            try
            {
                _intBuildKarma = Convert.ToInt32(objXmlCharacter["buildkarma"].InnerText);
                if (_intMaxKarma == 0)
                    _intMaxKarma = _intBuildKarma;
                if (_intBuildKarma == 35 && _strGameplayOption == "")
                {
                    _strGameplayOption = "Prime Runner";
                }
                if (_intBuildKarma == 35 && _intMaxNuyen == 0)
                {
                    _intMaxNuyen = 25;
                }
            }
            catch
            {
            }
			//Maximum number of Karma that can be spent/gained on Qualities.
			try
			{
				_intGameplayOptionQualityLimit = Convert.ToInt32(objXmlCharacter["gameplayoptionqualitylimit"].InnerText);
			}
			catch
			{
			}
			try
            {
                _objBuildMethod = ConvertToCharacterBuildMethod(objXmlCharacter["buildmethod"].InnerText);
            }
            catch
            {
            }
            try
            {
                _intKnowledgeSkills = Convert.ToInt32(objXmlCharacter["knowskillpts"].InnerText);

            }
            catch { }
            _intKnowledgeSkillPoints = Convert.ToInt32(objXmlCharacter["knowpts"].InnerText);
            _intSkillPoints = Convert.ToInt32(objXmlCharacter["skillpts"].InnerText);
            _intSkillPointsMaximum = Convert.ToInt32(objXmlCharacter["skillptsmax"].InnerText);
            _intSkillGroups = Convert.ToInt32(objXmlCharacter["skillgrps"].InnerText);
            _intSkillGroupsMaximum = Convert.ToInt32(objXmlCharacter["skillgrpsmax"].InnerText);
            _decNuyenBP = Convert.ToDecimal(objXmlCharacter["nuyenbp"].InnerText, GlobalOptions.Instance.CultureInfo);
            _decNuyenMaximumBP = Convert.ToDecimal(objXmlCharacter["nuyenmaxbp"].InnerText, GlobalOptions.Instance.CultureInfo);
            _blnAdeptEnabled = Convert.ToBoolean(objXmlCharacter["adept"].InnerText);
            _blnMagicianEnabled = Convert.ToBoolean(objXmlCharacter["magician"].InnerText);
            _blnTechnomancerEnabled = Convert.ToBoolean(objXmlCharacter["technomancer"].InnerText);
            try
            {
                _blnInitiationEnabled = Convert.ToBoolean(objXmlCharacter["initiationoverride"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnCritterEnabled = Convert.ToBoolean(objXmlCharacter["critter"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnUneducated = Convert.ToBoolean(objXmlCharacter["uneducated"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnUncouth = Convert.ToBoolean(objXmlCharacter["uncouth"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnSchoolOfHardKnocks = Convert.ToBoolean(objXmlCharacter["schoolofhardknocks"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnFriendsInHighPlaces = Convert.ToBoolean(objXmlCharacter["friendsinhighplaces"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnCollegeEducation = Convert.ToBoolean(objXmlCharacter["collegeeducation"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnJackOfAllTrades = Convert.ToBoolean(objXmlCharacter["jackofalltrades"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnInfirm = Convert.ToBoolean(objXmlCharacter["infirm"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnBlackMarket = Convert.ToBoolean(objXmlCharacter["blackmarket"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnExCon = Convert.ToBoolean(objXmlCharacter["excon"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnTrustFund = Convert.ToBoolean(objXmlCharacter["trustfund"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnTechSchool = Convert.ToBoolean(objXmlCharacter["techschool"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnRestrictedGear = Convert.ToBoolean(objXmlCharacter["restrictedgear"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnOverclocker = Convert.ToBoolean(objXmlCharacter["overclocker"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnMadeMan = Convert.ToBoolean(objXmlCharacter["mademan"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnLinguist = Convert.ToBoolean(objXmlCharacter["linguist"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnLightningReflexes = Convert.ToBoolean(objXmlCharacter["lightningreflexes"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnFame = Convert.ToBoolean(objXmlCharacter["fame"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnBornRich = Convert.ToBoolean(objXmlCharacter["bornrich"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnErased = Convert.ToBoolean(objXmlCharacter["erased"].InnerText);
            }
            catch
            {
            }
            _blnMAGEnabled = Convert.ToBoolean(objXmlCharacter["magenabled"].InnerText);
            try
            {
                _intInitiateGrade = Convert.ToInt32(objXmlCharacter["initiategrade"].InnerText);
            }
            catch
            {
            }
            _blnRESEnabled = Convert.ToBoolean(objXmlCharacter["resenabled"].InnerText);
            try
            {
                _intSubmersionGrade = Convert.ToInt32(objXmlCharacter["submersiongrade"].InnerText);
            }
            catch
            {
            }
            try
            {
                _blnGroupMember = Convert.ToBoolean(objXmlCharacter["groupmember"].InnerText);
            }
            catch
            {
            }
            try
            {
                _strGroupName = objXmlCharacter["groupname"].InnerText;
                _strGroupNotes = objXmlCharacter["groupnotes"].InnerText;
            }
            catch
            {
            }

            // Improvements.
            XmlNodeList objXmlImprovementList = objXmlDocument.SelectNodes("/character/improvements/improvement");
            foreach (XmlNode objXmlImprovement in objXmlImprovementList)
            {
                Improvement objImprovement = new Improvement();
                objImprovement.Load(objXmlImprovement);
                _lstImprovements.Add(objImprovement);
            }

            // Qualities
            objXmlNodeList = objXmlDocument.SelectNodes("/character/qualities/quality");
            bool blnHasOldQualities = false;
            foreach (XmlNode objXmlQuality in objXmlNodeList)
            {
                if (objXmlQuality["name"] != null)
                {
                    Quality objQuality = new Quality(this);
                    objQuality.Load(objXmlQuality);
                    _lstQualities.Add(objQuality);
                }
                else
                {
                    // If the Quality does not have a name tag, it is in the old format. Set the flag to show that old Qualities are in use.
                    blnHasOldQualities = true;
                }
            }
            // If old Qualities are in use, they need to be converted before we can continue.
            if (blnHasOldQualities)
                ConvertOldQualities(objXmlNodeList);

            // Attributes.
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"BOD\"]");
            _attBOD.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"AGI\"]");
            _attAGI.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"REA\"]");
            _attREA.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"STR\"]");
            _attSTR.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"CHA\"]");
            _attCHA.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"INT\"]");
            _attINT.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"LOG\"]");
            _attLOG.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"WIL\"]");
            _attWIL.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"INI\"]");
            _attINI.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"EDG\"]");
            _attEDG.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"MAG\"]");
            _attMAG.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"RES\"]");
            _attRES.Load(objXmlCharacter);
            objXmlCharacter = objXmlDocument.SelectSingleNode("/character/attributes/attribute[name = \"ESS\"]");
            _attESS.Load(objXmlCharacter);

            // A.I. Attributes.
            try
            {
                _intSignal = Convert.ToInt32(objXmlDocument.SelectSingleNode("/character/attributes/signal").InnerText);
                _intResponse = Convert.ToInt32(objXmlDocument.SelectSingleNode("/character/attributes/response").InnerText);
            }
            catch
            {
            }

            // Force.
            try
            {
                _intMaxSkillRating = Convert.ToInt32(objXmlDocument.SelectSingleNode("/character/attributes/maxskillrating").InnerText);
            }
            catch
            {
            }

            // Attempt to load the split MAG Attribute information for Mystic Adepts.
            if (_blnAdeptEnabled && _blnMagicianEnabled)
            {
                try
                {
                    _intMAGAdept = Convert.ToInt32(objXmlDocument.SelectSingleNode("/character/magsplitadept").InnerText);
                    _intMAGMagician = Convert.ToInt32(objXmlDocument.SelectSingleNode("/character/magsplitmagician").InnerText);
                }
                catch
                {
                }
            }

            // Attempt to load the Magic Tradition.
            try
            {
                _strMagicTradition = objXmlDocument.SelectSingleNode("/character/tradition").InnerText;
            }
            catch
            {
            }
            // Attempt to load the Magic Tradition Drain Attributes.
            try
            {
                _strTraditionDrain = objXmlDocument.SelectSingleNode("/character/traditiondrain").InnerText;
            }
            catch
            {
            }
            // Attempt to load the Magic Tradition Name.
            try
            {
                _strTraditionName = objXmlDocument.SelectSingleNode("/character/traditionname").InnerText;
            }
            catch
            {
            }
            // Attempt to load the Spirit Combat Name.
            try
            {
                _strSpiritCombat = objXmlDocument.SelectSingleNode("/character/spiritcombat").InnerText;
            }
            catch
            {
            }
            // Attempt to load the Spirit Detection Name.
            try
            {
                _strSpiritDetection = objXmlDocument.SelectSingleNode("/character/spiritdetection").InnerText;
            }
            catch
            {
            }
            // Attempt to load the Spirit Health Name.
            try
            {
                _strSpiritHealth = objXmlDocument.SelectSingleNode("/character/spirithealth").InnerText;
            }
            catch
            {
            }
            // Attempt to load the Spirit Illusion Name.
            try
            {
                _strSpiritIllusion = objXmlDocument.SelectSingleNode("/character/spiritillusion").InnerText;
            }
            catch
            {
            }
            // Attempt to load the Spirit Manipulation Name.
            try
            {
                _strSpiritManipulation = objXmlDocument.SelectSingleNode("/character/spiritmanipulation").InnerText;
            }
            catch
            {
            }
            // Attempt to load the Technomancer Stream.
            try
            {
                _strTechnomancerStream = objXmlDocument.SelectSingleNode("/character/stream").InnerText;
            }
            catch
            {
            }

            // Attempt to load Condition Monitor Progress.
            try
            {
                _intPhysicalCMFilled = Convert.ToInt32(objXmlDocument.SelectSingleNode("/character/physicalcmfilled").InnerText);
                _intStunCMFilled = Convert.ToInt32(objXmlDocument.SelectSingleNode("/character/stuncmfilled").InnerText);
            }
            catch
            {
            }

            // Skills.
            foreach (Skill objSkill in _lstSkills)
            {
                XmlNode objXmlSkill = objXmlDocument.SelectSingleNode("/character/skills/skill[name = \"" + objSkill.Name + "\"]");
                if (objXmlSkill != null)
                {
                    objSkill.Load(objXmlSkill);
                }
            }

            // Exotic Skills.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/skills/skill[exotic = \"True\"]");
            foreach (XmlNode objXmlSkill in objXmlNodeList)
            {
                Skill objSkill = new Skill(this);
                objSkill.Load(objXmlSkill);
                _lstSkills.Add(objSkill);
            }

            // SkillGroups.
            foreach (SkillGroup objGroup in _lstSkillGroups)
            {
                XmlNode objXmlSkill = objXmlDocument.SelectSingleNode("/character/skillgroups/skillgroup[name = \"" + objGroup.Name + "\"]");
                if (objXmlSkill != null)
                {
                    objGroup.Load(objXmlSkill);
                    // If the character is set to ignore rules or is in Career Mode, Skill Groups should have a maximum Rating of 6 unless they have been given a higher maximum Rating already.
                    if ((_blnIgnoreRules || _blnCreated) && objGroup.RatingMaximum < 12)
                        objGroup.RatingMaximum = 12;
                }
            }

            // Apply the broken skill group fix
            foreach (Skill objSkill in _lstSkills)
            {
                foreach (SkillGroup objGroup in _lstSkillGroups)
                {
                    if (objGroup.Broken && objGroup.Name == objSkill.SkillGroup)
                    {
                        objSkill.FreeLevels = objGroup.Rating;
                        objSkill.Base = objGroup.Rating;
                        objSkill.Karma = objSkill.Rating - objSkill.Base;
                    }
                }
            }

            foreach (SkillGroup objGroup in _lstSkillGroups)
            {
                if (objGroup.Base == 0 && objGroup.Karma == 0 && objGroup.Rating > 0)
                    objGroup.Base = objGroup.Rating;
            }

            // Knowledge Skills.
            List<ListItem> lstKnowledgeSkillOrder = new List<ListItem>();
            objXmlNodeList = objXmlDocument.SelectNodes("/character/skills/skill[knowledge = \"True\"]");
            // Sort the Knowledge Skills in alphabetical order.
            foreach (XmlNode objXmlSkill in objXmlNodeList)
            {
                ListItem objGroup = new ListItem();
                objGroup.Value = objXmlSkill["name"].InnerText;
                objGroup.Name = objXmlSkill["name"].InnerText;
                lstKnowledgeSkillOrder.Add(objGroup);
            }
            SortListItem objSort = new SortListItem();
            lstKnowledgeSkillOrder.Sort(objSort.Compare);

            foreach (ListItem objItem in lstKnowledgeSkillOrder)
            {
                Skill objSkill = new Skill(this);
                XmlNode objNode = objXmlDocument.SelectSingleNode("/character/skills/skill[knowledge = \"True\" and name = " + CleanXPath(objItem.Value) + "]");
                objSkill.Load(objNode);
                _lstSkills.Add(objSkill);
            }

            // Contacts.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/contacts/contact");
            foreach (XmlNode objXmlContact in objXmlNodeList)
            {
                Contact objContact = new Contact(this);
                objContact.Load(objXmlContact);
                _lstContacts.Add(objContact);
            }

            // Armor.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/armors/armor");
            foreach (XmlNode objXmlArmor in objXmlNodeList)
            {
                Armor objArmor = new Armor(this);
                objArmor.Load(objXmlArmor);
                _lstArmor.Add(objArmor);
            }

            // Weapons.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/weapons/weapon");
            foreach (XmlNode objXmlWeapon in objXmlNodeList)
            {
                Weapon objWeapon = new Weapon(this);
                objWeapon.Load(objXmlWeapon);
                _lstWeapons.Add(objWeapon);
            }

            // Cyberware/Bioware.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/cyberwares/cyberware");
            foreach (XmlNode objXmlCyberware in objXmlNodeList)
            {
                Cyberware objCyberware = new Cyberware(this);
                objCyberware.Load(objXmlCyberware);
                _lstCyberware.Add(objCyberware);
            }

            // Spells.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/spells/spell");
            foreach (XmlNode objXmlSpell in objXmlNodeList)
            {
                Spell objSpell = new Spell(this);
                objSpell.Load(objXmlSpell);
                _lstSpells.Add(objSpell);
            }

            // Foci.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/foci/focus");
            foreach (XmlNode objXmlFocus in objXmlNodeList)
            {
                Focus objFocus = new Focus();
                objFocus.Load(objXmlFocus);
                _lstFoci.Add(objFocus);
            }

            // Stacked Foci.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/stackedfoci/stackedfocus");
            foreach (XmlNode objXmlStack in objXmlNodeList)
            {
                StackedFocus objStack = new StackedFocus(this);
                objStack.Load(objXmlStack);
                _lstStackedFoci.Add(objStack);
            }

            // Powers.
            List<ListItem> lstPowerOrder = new List<ListItem>();
            objXmlNodeList = objXmlDocument.SelectNodes("/character/powers/power");
            // Sort the Powers in alphabetical order.
            foreach (XmlNode objXmlPower in objXmlNodeList)
            {
                ListItem objGroup = new ListItem();
                objGroup.Value = objXmlPower["extra"].InnerText;
                objGroup.Name = objXmlPower["name"].InnerText;

                lstPowerOrder.Add(objGroup);
            }
            objSort = new SortListItem();
            lstPowerOrder.Sort(objSort.Compare);

            foreach (ListItem objItem in lstPowerOrder)
            {
                Power objPower = new Power(this);
                XmlNode objNode = objXmlDocument.SelectSingleNode("/character/powers/power[name = " + CleanXPath(objItem.Name) + " and extra = " + CleanXPath(objItem.Value) + "]");
                objPower.Load(objNode);
                _lstPowers.Add(objPower);
            }

            // Spirits/Sprites.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/spirits/spirit");
            foreach (XmlNode objXmlSpirit in objXmlNodeList)
            {
                Spirit objSpirit = new Spirit(this);
                objSpirit.Load(objXmlSpirit);
                _lstSpirits.Add(objSpirit);
            }

            // Compex Forms/Technomancer Programs.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/complexforms/complexform");
            foreach (XmlNode objXmlProgram in objXmlNodeList)
            {
                ComplexForm objProgram = new ComplexForm(this);
                objProgram.Load(objXmlProgram);
                _lstComplexForms.Add(objProgram);
            }

            // Martial Arts.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/martialarts/martialart");
            foreach (XmlNode objXmlArt in objXmlNodeList)
            {
                MartialArt objMartialArt = new MartialArt(this);
                objMartialArt.Load(objXmlArt);
                _lstMartialArts.Add(objMartialArt);
            }

            // Martial Art Maneuvers.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/martialartmaneuvers/martialartmaneuver");
            foreach (XmlNode objXmlManeuver in objXmlNodeList)
            {
                MartialArtManeuver objManeuver = new MartialArtManeuver(this);
                objManeuver.Load(objXmlManeuver);
                _lstMartialArtManeuvers.Add(objManeuver);
            }

            // Limit Modifiers.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/limitmodifiers/limitmodifier");
            foreach (XmlNode objXmlLimit in objXmlNodeList)
            {
                LimitModifier obLimitModifier = new LimitModifier(this);
                obLimitModifier.Load(objXmlLimit);
                _lstLimitModifiers.Add(obLimitModifier);
            }

            // Lifestyles.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/lifestyles/lifestyle");
            foreach (XmlNode objXmlLifestyle in objXmlNodeList)
            {
                Lifestyle objLifestyle = new Lifestyle(this);
                objLifestyle.Load(objXmlLifestyle);
                _lstLifestyles.Add(objLifestyle);
            }

            // <gears>
            objXmlNodeList = objXmlDocument.SelectNodes("/character/gears/gear");
            foreach (XmlNode objXmlGear in objXmlNodeList)
            {
                switch (objXmlGear["category"].InnerText)
                {
                    case "Commlinks":
                    case "Cyberdecks":
                    case "Rigger Command Consoles":
                        Commlink objCommlink = new Commlink(this);
                        objCommlink.Load(objXmlGear);
                        _lstGear.Add(objCommlink);
                        break;
                    default:
                        Gear objGear = new Gear(this);
                        objGear.Load(objXmlGear);
                        _lstGear.Add(objGear);
                        break;
                }
            }

            // Vehicles.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/vehicles/vehicle");
            foreach (XmlNode objXmlVehicle in objXmlNodeList)
            {
                Vehicle objVehicle = new Vehicle(this);
                objVehicle.Load(objXmlVehicle);
                _lstVehicles.Add(objVehicle);
            }

            // Metamagics/Echoes.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/metamagics/metamagic");
            foreach (XmlNode objXmlMetamagic in objXmlNodeList)
            {
                Metamagic objMetamagic = new Metamagic(this);
                objMetamagic.Load(objXmlMetamagic);
                _lstMetamagics.Add(objMetamagic);
            }

            // Arts
            objXmlNodeList = objXmlDocument.SelectNodes("/character/arts/art");
            foreach (XmlNode objXmlArt in objXmlNodeList)
            {
                Art objArt = new Art(this);
                objArt.Load(objXmlArt);
                _lstArts.Add(objArt);
            }

            // Enhancements
            objXmlNodeList = objXmlDocument.SelectNodes("/character/enhancements/enhancement");
            foreach (XmlNode objXmlEnhancement in objXmlNodeList)
            {
                Enhancement objEnhancement = new Enhancement(this);
                objEnhancement.Load(objXmlEnhancement);
                _lstEnhancements.Add(objEnhancement);
            }

            // Critter Powers.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/critterpowers/critterpower");
            foreach (XmlNode objXmlPower in objXmlNodeList)
            {
                CritterPower objPower = new CritterPower(this);
                objPower.Load(objXmlPower);
                _lstCritterPowers.Add(objPower);
            }

            // Initiation Grades.
            objXmlNodeList = objXmlDocument.SelectNodes("/character/initiationgrades/initiationgrade");
            foreach (XmlNode objXmlGrade in objXmlNodeList)
            {
                InitiationGrade objGrade = new InitiationGrade(this);
                objGrade.Load(objXmlGrade);
                _lstInitiationGrades.Add(objGrade);
            }

            // Expense Log Entries.
            XmlNodeList objXmlExpenseList = objXmlDocument.SelectNodes("/character/expenses/expense");
            foreach (XmlNode objXmlExpense in objXmlExpenseList)
            {
                ExpenseLogEntry objExpenseLogEntry = new ExpenseLogEntry();
                objExpenseLogEntry.Load(objXmlExpense);
                _lstExpenseLog.Add(objExpenseLogEntry);
            }

            // Locations.
            XmlNodeList objXmlLocationList = objXmlDocument.SelectNodes("/character/locations/location");
            foreach (XmlNode objXmlLocation in objXmlLocationList)
            {
                _lstLocations.Add(objXmlLocation.InnerText);
            }

            // Armor Bundles.
            XmlNodeList objXmlBundleList = objXmlDocument.SelectNodes("/character/armorbundles/armorbundle");
            foreach (XmlNode objXmlBundle in objXmlBundleList)
            {
                _lstArmorBundles.Add(objXmlBundle.InnerText);
            }

            // Weapon Locations.
            XmlNodeList objXmlWeaponLocationList = objXmlDocument.SelectNodes("/character/weaponlocations/weaponlocation");
            foreach (XmlNode objXmlLocation in objXmlWeaponLocationList)
            {
                _lstWeaponLocations.Add(objXmlLocation.InnerText);
            }

            // Improvement Groups.
            XmlNodeList objXmlGroupList = objXmlDocument.SelectNodes("/character/improvementgroups/improvementgroup");
            foreach (XmlNode objXmlGroup in objXmlGroupList)
            {
                _lstImprovementGroups.Add(objXmlGroup.InnerText);
            }

            // Calendar.
            XmlNodeList objXmlWeekList = objXmlDocument.SelectNodes("/character/calendar/week");
            foreach (XmlNode objXmlWeek in objXmlWeekList)
            {
                CalendarWeek objWeek = new CalendarWeek();
                objWeek.Load(objXmlWeek);
                _lstCalendar.Add(objWeek);
            }

            // Look for the unarmed attack
            bool blnFoundUnarmed = false;
            foreach (Weapon objWeapon in _lstWeapons)
            {
                if (objWeapon.Name == "Unarmed Attack")
                    blnFoundUnarmed = true;
            }

            if (!blnFoundUnarmed)
            {
                // Add the Unarmed Attack Weapon to the character.
                try
                {
                    XmlDocument objXmlWeaponDoc = XmlManager.Instance.Load("weapons.xml");
                    XmlNode objXmlWeapon = objXmlWeaponDoc.SelectSingleNode("/chummer/weapons/weapon[name = \"Unarmed Attack\"]");
                    TreeNode objGearWeaponNode = new TreeNode();
                    Weapon objWeapon = new Weapon(this);
                    objWeapon.Create(objXmlWeapon, this, objGearWeaponNode, null, null, null);
                    objGearWeaponNode.ForeColor = SystemColors.GrayText;
                    _lstWeapons.Add(objWeapon);
                }
                catch
                {
                }
            }

            // converting from old dwarven resistance to new dwarven resistance
            if (this.Metatype.ToLower().Equals("dwarf")
                && this.Qualities.Where(x => x.Name.Equals("Dwarf Resistance")).FirstOrDefault() == null
                && this.Qualities.Where(x => x.Name.Equals("Resistance to Pathogens and Toxins")).FirstOrDefault() != null)
            {
                this.Qualities.Remove(this.Qualities.Where(x => x.Name.Equals("Resistance to Pathogens and Toxins")).First());
                XmlNode objXmlDwarfQuality = XmlManager.Instance.Load("qualities.xml").SelectSingleNode("/chummer/qualities/quality[name = \"Dwarf Resistance\"]");

                TreeNode objNode = new TreeNode();
                List<Weapon> objWeapons = new List<Weapon>();
                List<TreeNode> objWeaponNodes = new List<TreeNode>();
                Quality objQuality = new Quality(this);

                objQuality.Create(objXmlDwarfQuality, this, QualitySource.Metatype, objNode, objWeapons, objWeaponNodes);
                this._lstQualities.Add(objQuality);
                blnHasOldQualities = true;
            }


            // load issue where the contact multiplier was set to 0
            if (_intContactMultiplier == 0 && _strGameplayOption != string.Empty)
            {
                XmlDocument objXmlDocumentPriority = XmlManager.Instance.Load("gameplayoptions.xml");
                XmlNode objXmlGameplayOption = objXmlDocumentPriority.SelectSingleNode("/chummer/gameplayoptions/gameplayoption[name = \"" + _strGameplayOption + "\"]");
                string strKarma = objXmlGameplayOption["karma"].InnerText;
                string strNuyen = objXmlGameplayOption["maxnuyen"].InnerText;
                string strContactMultiplier = "0";
                if (!_objOptions.FreeContactsMultiplierEnabled)
                {

                    strContactMultiplier = objXmlGameplayOption["contactmultiplier"].InnerText;
                }
                else
                {
                    strContactMultiplier = _objOptions.FreeContactsMultiplier.ToString();
                }
                _intMaxKarma = Convert.ToInt32(strKarma);
                _intMaxNuyen = Convert.ToInt32(strNuyen);
                _intContactMultiplier = Convert.ToInt32(strContactMultiplier);
                _intContactPoints = (CHA.Base + CHA.Karma) * _intContactMultiplier;
            }

            // If the character had old Qualities that were converted, immediately save the file so they are in the new format.
            if (blnHasOldQualities)
                Save();

            return true;
        }

        /// <summary>
        /// Print this character information to a MemoryStream. This creates only the character object itself, not any of the opening or closing XmlDocument items.
        /// This can be used to write multiple characters to a single XmlDocument.
        /// </summary>
        /// <param name="objStream">MemoryStream to use.</param>
        /// <param name="objWriter">XmlTextWriter to write to.</param>
        public void PrintToStream(MemoryStream objStream, XmlTextWriter objWriter)
        {
            XmlDocument objXmlDocument = new XmlDocument();

            XmlDocument objMetatypeDoc = new XmlDocument();
            XmlNode objMetatypeNode;
            string strMetatype = "";
            string strMetavariant = "";
            // Get the name of the Metatype and Metavariant.
            objMetatypeDoc = XmlManager.Instance.Load("metatypes.xml");
            {
                objMetatypeNode = objMetatypeDoc.SelectSingleNode("/chummer/metatypes/metatype[name = \"" + _strMetatype + "\"]");
                if (objMetatypeNode == null)
                    objMetatypeDoc = XmlManager.Instance.Load("critters.xml");
                objMetatypeNode = objMetatypeDoc.SelectSingleNode("/chummer/metatypes/metatype[name = \"" + _strMetatype + "\"]");

                if (objMetatypeNode["translate"] != null)
                    strMetatype = objMetatypeNode["translate"].InnerText;
                else
                    strMetatype = _strMetatype;

                if (_strMetavariant != "")
                {
                    objMetatypeNode = objMetatypeNode.SelectSingleNode("metavariants/metavariant[name = \"" + _strMetavariant + "\"]");

                    if (objMetatypeNode["translate"] != null)
                        strMetavariant = objMetatypeNode["translate"].InnerText;
                    else
                        strMetavariant = _strMetavariant;
                }
            }

            Guid guiImage = new Guid();
            guiImage = Guid.NewGuid();
            // This line left in for debugging. Write the output to a fixed file name.
            //FileStream objStream = new FileStream("D:\\temp\\print.xml", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);//(_strFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

            // <character>
            objWriter.WriteStartElement("character");

            // <metatype />
            objWriter.WriteElementString("metatype", strMetatype);
            // <metatype_english />
            objWriter.WriteElementString("metatype_english", _strMetatype);
            // <metavariant />
            objWriter.WriteElementString("metavariant", strMetavariant);
            // <metavariant_english />
            objWriter.WriteElementString("metavariant_english", _strMetavariant);
            // <movement />
            objWriter.WriteElementString("movement", FullMovement());
            // <walk />
            objWriter.WriteElementString("walk", FullMovement());
            // <run />
            objWriter.WriteElementString("run", FullMovement());
            // <sprint />
            objWriter.WriteElementString("sprint", FullMovement());
            // <movementwalk />
            objWriter.WriteElementString("movementwalk", Movement);
            // <movementswim />
            objWriter.WriteElementString("movementswim", Swim);
            // <movementfly />
            objWriter.WriteElementString("movementfly", Fly);

            // <gameplayoption />
            objWriter.WriteElementString("gameplayoption", _strGameplayOption);
            // <maxkarma />
            objWriter.WriteElementString("maxkarma", _intMaxKarma.ToString());
            // <maxnuyen />
            objWriter.WriteElementString("maxnuyen", _intMaxKarma.ToString());
            // <contactmultiplier />
            objWriter.WriteElementString("contactmultiplier", _intContactMultiplier.ToString());
            // <prioritymetatype />
            objWriter.WriteElementString("prioritymetatype", _strPriorityMetatype);
            // <priorityattributes />
            objWriter.WriteElementString("priorityattributes", _strPriorityAttributes);
            // <priorityspecial />
            objWriter.WriteElementString("priorityspecial", _strPrioritySpecial);
            // <priorityskills />
            objWriter.WriteElementString("priorityskills", _strPrioritySkills);
            // <priorityresources />
            objWriter.WriteElementString("priorityresources", _strPriorityResources);
            // <priorityskill1 />
            objWriter.WriteElementString("priorityskill1", _strSkill1);
            // <priorityskill2 />
            objWriter.WriteElementString("priorityskill2", _strSkill2);
            // <priorityskillgroup />
            objWriter.WriteElementString("priorityskillgroup", _strSkillGroup);

            // If the character does not have a name, call them Unnamed Character. This prevents a transformed document from having a self-terminated title tag which causes browser to not rendering anything.
            // <name />
            if (_strName != "")
                objWriter.WriteElementString("name", _strName);
            else
                objWriter.WriteElementString("name", LanguageManager.Instance.GetString("String_UnnamedCharacter"));

            // Since IE is retarded and can't handle base64 images before IE9, we need to dump the image to a temporary directory and re-write the information.
            // If you give it an extension of jpg, gif, or png, it expects the file to be in that format and won't render the image unless it was originally that type.
            // But if you give it the extension img, it will render whatever you give it (which doesn't make any damn sense, but that's IE for you).
            string strMugshotPath = "";
            if (_strMugshot != "")
            {
                if (!Directory.Exists(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "mugshots"))
                    Directory.CreateDirectory(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "mugshots");
                byte[] bytImage = Convert.FromBase64String(_strMugshot);
                MemoryStream objImageStream = new MemoryStream(bytImage, 0, bytImage.Length);
                objImageStream.Write(bytImage, 0, bytImage.Length);
                Image imgMugshot = Image.FromStream(objImageStream, true);
                imgMugshot.Save(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "mugshots" + Path.DirectorySeparatorChar + guiImage.ToString() + ".img");
                strMugshotPath = "file://" + (Environment.CurrentDirectory + Path.DirectorySeparatorChar + "mugshots" + Path.DirectorySeparatorChar + guiImage.ToString() + ".img").Replace(Path.DirectorySeparatorChar, '/');
            }
            // <mugshot />
            objWriter.WriteElementString("mugshot", strMugshotPath);
            // <mugshotbase64 />
            objWriter.WriteElementString("mugshotbase64", _strMugshot);
            // <sex />
            objWriter.WriteElementString("sex", _strSex);
            // <age />
            objWriter.WriteElementString("age", _strAge);
            // <eyes />
            objWriter.WriteElementString("eyes", _strEyes);
            // <height />
            objWriter.WriteElementString("height", _strHeight);
            // <weight />
            objWriter.WriteElementString("weight", _strWeight);
            // <skin />
            objWriter.WriteElementString("skin", _strSkin);
            // <hair />
            objWriter.WriteElementString("hair", _strHair);
            // <description />
            objWriter.WriteElementString("description", _strDescription);
            // <background />
            objWriter.WriteElementString("background", _strBackground);
            // <concept />
            objWriter.WriteElementString("concept", _strConcept);
            // <notes />
            objWriter.WriteElementString("notes", _strNotes);
            // <alias />
            objWriter.WriteElementString("alias", _strAlias);
            // <playername />
            objWriter.WriteElementString("playername", _strPlayerName);
            // <gamenotes />
            objWriter.WriteElementString("gamenotes", _strGameNotes);

            // <limitphysical />
            objWriter.WriteElementString("limitphysical", LimitPhysical.ToString());
            // <limitmental />
            objWriter.WriteElementString("limitmental", LimitMental.ToString());
            // <limitsocial />
            objWriter.WriteElementString("limitsocial", LimitSocial.ToString());
            // <limitastral />
            objWriter.WriteElementString("limitastral", LimitAstral.ToString());
            // <contactpoints />
            objWriter.WriteElementString("contactpoints", _intContactPoints.ToString());
            // <contactpointsused />
            objWriter.WriteElementString("contactpointsused", _intContactPointsUsed.ToString());
            // <cfplimit />
            objWriter.WriteElementString("cfplimit", _intCFPLimit.ToString());
            // <spelllimit />
            objWriter.WriteElementString("spelllimit", _intSpellLimit.ToString());
            // <karma />
            objWriter.WriteElementString("karma", _intKarma.ToString());
            // <totalkarma />
            objWriter.WriteElementString("totalkarma", String.Format("{0:###,###,##0}", Convert.ToInt32(CareerKarma)));
            // <special />
            objWriter.WriteElementString("special", _intSpecial.ToString());
            // <totalspecial />
            objWriter.WriteElementString("totalspecial", String.Format("{0:###,###,##0}", Convert.ToInt32(_intTotalSpecial)));
            // <attributes />
            objWriter.WriteElementString("attributes", _intSpecial.ToString());
            // <totalattributes />
            objWriter.WriteElementString("totalattributes", String.Format("{0:###,###,##0}", Convert.ToInt32(_intTotalAttributes)));
            // <streetcred />
            objWriter.WriteElementString("streetcred", _intStreetCred.ToString());
            // <calculatedstreetcred />
            objWriter.WriteElementString("calculatedstreetcred", CalculatedStreetCred.ToString());
            // <totalstreetcred />
            objWriter.WriteElementString("totalstreetcred", TotalStreetCred.ToString());
            // <burntstreetcred />
            objWriter.WriteElementString("burntstreetcred", _intBurntStreetCred.ToString());
            // <notoriety />
            objWriter.WriteElementString("notoriety", _intNotoriety.ToString());
            // <calculatednotoriety />
            objWriter.WriteElementString("calculatednotoriety", CalculatedNotoriety.ToString());
            // <totalnotoriety />
            objWriter.WriteElementString("totalnotoriety", TotalNotoriety.ToString());
            // <publicawareness />
            objWriter.WriteElementString("publicawareness", _intPublicAwareness.ToString());
            // <calculatedpublicawareness />
            objWriter.WriteElementString("calculatedpublicawareness", CalculatedPublicAwareness.ToString());
            // <totalpublicawareness />
            objWriter.WriteElementString("totalpublicawareness", TotalPublicAwareness.ToString());
            // <created />
            objWriter.WriteElementString("created", _blnCreated.ToString());
            // <nuyen />
            objWriter.WriteElementString("nuyen", _intNuyen.ToString());
            // <adeptwaydiscount />
            objWriter.WriteElementString("adeptwaydiscount", _intAdeptWayDiscount.ToString());
            int i = this._intNuyen;
            // <adept />
            objWriter.WriteElementString("adept", _blnAdeptEnabled.ToString());
            // <magician />
            objWriter.WriteElementString("magician", _blnMagicianEnabled.ToString());
            // <technomancer />
            objWriter.WriteElementString("technomancer", _blnTechnomancerEnabled.ToString());
            // <critter />
            objWriter.WriteElementString("critter", _blnCritterEnabled.ToString());

            // <tradition />
            string strTraditionName = _strMagicTradition;
            if (strTraditionName == "Custom")
                strTraditionName = _strTraditionName;
			objWriter.WriteStartElement("tradition");
            objWriter.WriteElementString("name", strTraditionName);

            if (_strMagicTradition != "")
            {
                string strDrainAtt = "";
                objXmlDocument = new XmlDocument();
                objXmlDocument = XmlManager.Instance.Load("traditions.xml");

                XmlNode objXmlTradition = objXmlDocument.SelectSingleNode("/chummer/traditions/tradition[name = \"" + _strMagicTradition + "\"]");

                try
                {
                    if (objXmlTradition["name"].InnerText == "Custom")
                        strDrainAtt = _strTraditionDrain;
                    else
                        strDrainAtt = objXmlTradition["drain"].InnerText;
                }
                catch
                {
                    strDrainAtt = objXmlTradition["drain"].InnerText;
                }

                XPathNavigator nav = objXmlDocument.CreateNavigator();
                string strDrain = strDrainAtt.Replace("BOD", _attBOD.TotalValue.ToString());
                strDrain = strDrain.Replace("AGI", _attAGI.TotalValue.ToString());
                strDrain = strDrain.Replace("REA", _attREA.TotalValue.ToString());
                strDrain = strDrain.Replace("STR", _attSTR.TotalValue.ToString());
                strDrain = strDrain.Replace("CHA", _attCHA.TotalValue.ToString());
                strDrain = strDrain.Replace("INT", _attINT.TotalValue.ToString());
                strDrain = strDrain.Replace("LOG", _attLOG.TotalValue.ToString());
                strDrain = strDrain.Replace("WIL", _attWIL.TotalValue.ToString());
                strDrain = strDrain.Replace("MAG", _attMAG.TotalValue.ToString());
                XPathExpression xprDrain = nav.Compile(strDrain);

                // Add any Improvements for Drain Resistance.
                int intDrain = Convert.ToInt32(nav.Evaluate(xprDrain)) + _objImprovementManager.ValueOf(Improvement.ImprovementType.DrainResistance);

                objWriter.WriteElementString("drain", strDrainAtt + " (" + intDrain.ToString() + ")");

				// Get spirit type per spell category
				XmlNodeList lstXmlSpiritTypes = objXmlTradition.SelectNodes("spirits/spirit");
				
				string strSpiritCombat = lstXmlSpiritTypes[0] != null ? lstXmlSpiritTypes[0].InnerText : _strSpiritCombat;
				string strSpiritDetection = lstXmlSpiritTypes[1] != null ? lstXmlSpiritTypes[1].InnerText : _strSpiritDetection;
				string strSpiritHealth = lstXmlSpiritTypes[2] != null ? lstXmlSpiritTypes[2].InnerText : _strSpiritHealth;
				string strSpiritIllusion = lstXmlSpiritTypes[3] != null ? lstXmlSpiritTypes[3].InnerText : _strSpiritIllusion;
				string strSpiritManipulation = lstXmlSpiritTypes[4] != null ? lstXmlSpiritTypes[4].InnerText : _strSpiritManipulation;

				objWriter.WriteElementString("spiritcombat", strSpiritCombat);
				objWriter.WriteElementString("spiritdetection", strSpiritDetection);
				objWriter.WriteElementString("spirithealth", strSpiritHealth);
				objWriter.WriteElementString("spiritillusion", strSpiritIllusion);
				objWriter.WriteElementString("spiritmanipulation", strSpiritManipulation);

				//Spirit form, default to materialization unless field with other data persists
				string strSpiritForm;
				objXmlTradition.TryGetField("spiritform", out strSpiritForm, "Materialization");
				objWriter.WriteElementString("spiritform", strSpiritForm);

				//Rulebook reference
				string strSource, strPage;
				objXmlTradition.TryGetField("source", out strSource);
				objXmlTradition.TryGetField("page", out strPage);

				objWriter.WriteElementString("source", strSource);
				objWriter.WriteElementString("page", strPage);
            }
			objWriter.WriteEndElement();

			// <stream />
			objWriter.WriteElementString("stream", _strTechnomancerStream);
            if (_strTechnomancerStream != "")
            {
                string strDrainAtt = "";
                objXmlDocument = new XmlDocument();
                objXmlDocument = XmlManager.Instance.Load("streams.xml");

                XmlNode objXmlTradition = objXmlDocument.SelectSingleNode("/chummer/traditions/tradition[name = \"" + _strTechnomancerStream + "\"]");
                strDrainAtt = objXmlTradition["drain"].InnerText;

                XPathNavigator nav = objXmlDocument.CreateNavigator();
                string strDrain = strDrainAtt.Replace("BOD", _attBOD.TotalValue.ToString());
                strDrain = strDrain.Replace("AGI", _attAGI.TotalValue.ToString());
                strDrain = strDrain.Replace("REA", _attREA.TotalValue.ToString());
                strDrain = strDrain.Replace("STR", _attSTR.TotalValue.ToString());
                strDrain = strDrain.Replace("CHA", _attCHA.TotalValue.ToString());
                strDrain = strDrain.Replace("INT", _attINT.TotalValue.ToString());
                strDrain = strDrain.Replace("LOG", _attLOG.TotalValue.ToString());
                strDrain = strDrain.Replace("WIL", _attWIL.TotalValue.ToString());
                strDrain = strDrain.Replace("RES", _attRES.TotalValue.ToString());
                XPathExpression xprDrain = nav.Compile(strDrain);

                // Add any Improvements for Fading Resistance.
                int intDrain = Convert.ToInt32(nav.Evaluate(xprDrain)) + _objImprovementManager.ValueOf(Improvement.ImprovementType.FadingResistance);

                objWriter.WriteElementString("drain", strDrainAtt + " (" + intDrain.ToString() + ")");
            }

            // <attributes>
            objWriter.WriteStartElement("attributes");
            _attBOD.Print(objWriter);
            _attAGI.Print(objWriter);
            _attREA.Print(objWriter);
            _attSTR.Print(objWriter);
            _attCHA.Print(objWriter);
            _attINT.Print(objWriter);
            _attLOG.Print(objWriter);
            _attWIL.Print(objWriter);
            _attINI.Print(objWriter);
            _attEDG.Print(objWriter);
            _attMAG.Print(objWriter);
            _attRES.Print(objWriter);
            if (_strMetatype.EndsWith("A.I.") || _strMetatypeCategory == "Technocritters" || _strMetatypeCategory == "Protosapients")
            {
                objWriter.WriteElementString("signal", _intSignal.ToString());
                objWriter.WriteElementString("response", _intResponse.ToString());
                objWriter.WriteElementString("system", System.ToString());
                objWriter.WriteElementString("firewall", Firewall.ToString());
                objWriter.WriteElementString("rating", Rating.ToString());
            }

            objWriter.WriteStartElement("attribute");
            objWriter.WriteElementString("name", "ESS");
            objWriter.WriteElementString("base", Essence.ToString());
            objWriter.WriteEndElement();

            // </attributes>
            objWriter.WriteEndElement();

            // <armor />
            objWriter.WriteElementString("armor", TotalArmorRating.ToString());

            // Condition Monitors.
            // <physicalcm />
            objWriter.WriteElementString("physicalcm", PhysicalCM.ToString());
            // <stuncm />
            objWriter.WriteElementString("stuncm", StunCM.ToString());

            // Condition Monitor Progress.
            // <physicalcmfilled />
            objWriter.WriteElementString("physicalcmfilled", _intPhysicalCMFilled.ToString());
            // <stuncmfilled />
            objWriter.WriteElementString("stuncmfilled", _intStunCMFilled.ToString());

            // <cmthreshold>
            objWriter.WriteElementString("cmthreshold", CMThreshold.ToString());
            // <cmthresholdoffset>
            objWriter.WriteElementString("cmthresholdoffset", CMThresholdOffset.ToString());
            // <cmoverflow>
            objWriter.WriteElementString("cmoverflow", CMOverflow.ToString());

            // Calculate Initiatives.
            // Initiative.
            string strInit = this.Initiative;
            objWriter.WriteElementString("init", strInit);

            // Astral Initiative.
            if (this.MAGEnabled)
            {
                string strAstralInit = this.AstralInitiative;
                objWriter.WriteElementString("astralinit", strAstralInit);
            }

            // Matrix Initiative (AR).
            string strMatrixInitAR = this.MatrixInitiative;
            objWriter.WriteElementString("matrixarinit", strMatrixInitAR);

            // Matrix Initiative (Cold).
            string strMatrixInitCold = this.MatrixInitiativeCold;
            objWriter.WriteElementString("matrixcoldinit", strMatrixInitCold);

            // Matrix Initiative (Hot).
            string strMatrixInitHot = this.MatrixInitiativeHot;
            objWriter.WriteElementString("matrixhotinit", strMatrixInitHot);

            // Rigger Initiative.
            string strRiggerInit = this.RiggerInitiative;
            objWriter.WriteElementString("riggerinit", strRiggerInit);

            // <magenabled />
            objWriter.WriteElementString("magenabled", _blnMAGEnabled.ToString());
            // <initiategrade />
            objWriter.WriteElementString("initiategrade", _intInitiateGrade.ToString());
            // <resenabled />
            objWriter.WriteElementString("resenabled", _blnRESEnabled.ToString());
            // <submersiongrade />
            objWriter.WriteElementString("submersiongrade", _intSubmersionGrade.ToString());
            // <groupmember />
            objWriter.WriteElementString("groupmember", _blnGroupMember.ToString());
            // <groupname />
            objWriter.WriteElementString("groupname", _strGroupName);
            // <groupnotes />
            objWriter.WriteElementString("groupnotes", _strGroupNotes);

            // <composure />
            objWriter.WriteElementString("composure", Composure.ToString());
            // <judgeintentions />
            objWriter.WriteElementString("judgeintentions", JudgeIntentions.ToString());
            // <liftandcarry />
            objWriter.WriteElementString("liftandcarry", LiftAndCarry.ToString());
            // <memory />
            objWriter.WriteElementString("memory", Memory.ToString());
            // <liftweight />
            objWriter.WriteElementString("liftweight", (_attSTR.TotalValue * 15).ToString());
            // <carryweight />
            objWriter.WriteElementString("carryweight", (_attSTR.TotalValue * 10).ToString());

            // <skills>
            objWriter.WriteStartElement("skills");
            foreach (Skill objSkill in _lstSkills)
            {
                if (_objOptions.PrintSkillsWithZeroRating || (!_objOptions.PrintSkillsWithZeroRating && objSkill.Rating > 0) || objSkill.KnowledgeSkill)
                    objSkill.Print(objWriter);
            }
            // </skills>
            objWriter.WriteEndElement();

            // <contacts>
            objWriter.WriteStartElement("contacts");
            foreach (Contact objContact in _lstContacts)
            {
                objContact.Print(objWriter);
            }
            // </contacts>
            objWriter.WriteEndElement();

            // <limitmodifiersphys>
            objWriter.WriteStartElement("limitmodifiersphys");
            foreach (LimitModifier objLimitModifier in _lstLimitModifiers)
            {
                if (objLimitModifier.Limit == "Physical")
                    objLimitModifier.Print(objWriter);
            }
            // Populate Limit Modifiers from Improvements
            foreach (Improvement objImprovement in _lstImprovements)
            {
                if (objImprovement.ImproveType == Improvement.ImprovementType.LimitModifier)
                {
                    if (objImprovement.ImprovedName == "Physical")
                    {
                        string strName = objImprovement.UniqueName;
                        if (objImprovement.Value > 0)
                            strName += " [+" + objImprovement.Value.ToString() + "]";
                        else
                            strName += " [" + objImprovement.Value.ToString() + "]";

                        if (objImprovement.Exclude != "")
                            strName += " (" + objImprovement.Exclude + ")";

                        objWriter.WriteStartElement("limitmodifier");
                        objWriter.WriteElementString("name", strName);
                        if (this.Options.PrintNotes)
                            objWriter.WriteElementString("notes", objImprovement.Notes);
                        objWriter.WriteEndElement();
                    }
                }
            }
            // </limitmodifiersphys>
            objWriter.WriteEndElement();

            // <limitmodifiersment>
            objWriter.WriteStartElement("limitmodifiersment");
            foreach (LimitModifier objLimitModifier in _lstLimitModifiers)
            {
                if (objLimitModifier.Limit == "Mental")
                    objLimitModifier.Print(objWriter);
            }
            // Populate Limit Modifiers from Improvements
            foreach (Improvement objImprovement in _lstImprovements)
            {
                if (objImprovement.ImproveType == Improvement.ImprovementType.LimitModifier)
                {
                    if (objImprovement.ImprovedName == "Mental")
                    {
                        string strName = objImprovement.UniqueName;
                        if (objImprovement.Value > 0)
                            strName += " [+" + objImprovement.Value.ToString() + "]";
                        else
                            strName += " [" + objImprovement.Value.ToString() + "]";

                        if (objImprovement.Exclude != "")
                            strName += " (" + objImprovement.Exclude + ")";

                        objWriter.WriteStartElement("limitmodifier");
                        objWriter.WriteElementString("name", strName);
                        if (this.Options.PrintNotes)
                            objWriter.WriteElementString("notes", objImprovement.Notes);
                        objWriter.WriteEndElement();
                    }
                }
            }
            // </limitmodifiersment>
            objWriter.WriteEndElement();

            // <limitmodifierssoc>
            objWriter.WriteStartElement("limitmodifierssoc");
            foreach (LimitModifier objLimitModifier in _lstLimitModifiers)
            {
                if (objLimitModifier.Limit == "Social")
                    objLimitModifier.Print(objWriter);
            }
            // Populate Limit Modifiers from Improvements
            foreach (Improvement objImprovement in _lstImprovements)
            {
                if (objImprovement.ImproveType == Improvement.ImprovementType.LimitModifier)
                {
                    if (objImprovement.ImprovedName == "Social")
                    {
                        string strName = objImprovement.UniqueName;
                        if (objImprovement.Value > 0)
                            strName += " [+" + objImprovement.Value.ToString() + "]";
                        else
                            strName += " [" + objImprovement.Value.ToString() + "]";

                        if (objImprovement.Exclude != "")
                            strName += " (" + objImprovement.Exclude + ")";

                        objWriter.WriteStartElement("limitmodifier");
                        objWriter.WriteElementString("name", strName);
                        if (this.Options.PrintNotes)
                            objWriter.WriteElementString("notes", objImprovement.Notes);
                        objWriter.WriteEndElement();
                    }
                }
            }
            // </limitmodifierssoc>
            objWriter.WriteEndElement();

            // <spells>
            objWriter.WriteStartElement("spells");
            foreach (Spell objSpell in _lstSpells)
            {
                objSpell.Print(objWriter);
            }
            // </spells>
            objWriter.WriteEndElement();

            // <powers>
            objWriter.WriteStartElement("powers");
            foreach (Power objPower in _lstPowers)
            {
                objPower.Print(objWriter);
            }
            // </powers>
            objWriter.WriteEndElement();

            // <spirits>
            objWriter.WriteStartElement("spirits");
            foreach (Spirit objSpirit in _lstSpirits)
            {
                objSpirit.Print(objWriter);
            }
            // </spirits>
            objWriter.WriteEndElement();

            // <complexforms>
            objWriter.WriteStartElement("complexforms");
            foreach (ComplexForm objProgram in _lstComplexForms)
            {
                objProgram.Print(objWriter);
            }
            // </complexforms>
            objWriter.WriteEndElement();

            // <martialarts>
            objWriter.WriteStartElement("martialarts");
            foreach (MartialArt objMartialArt in _lstMartialArts)
            {
                objMartialArt.Print(objWriter);
            }
            // </martialarts>
            objWriter.WriteEndElement();

            // <martialartmaneuvers>
            objWriter.WriteStartElement("martialartmaneuvers");
            foreach (MartialArtManeuver objManeuver in _lstMartialArtManeuvers)
            {
                objManeuver.Print(objWriter);
            }
            // </martialartmaneuvers>
            objWriter.WriteEndElement();

            // <armors>
            objWriter.WriteStartElement("armors");
            foreach (Armor objArmor in _lstArmor)
            {
                objArmor.Print(objWriter);
            }
            // </armors>
            objWriter.WriteEndElement();

            // <weapons>
            objWriter.WriteStartElement("weapons");
            foreach (Weapon objWeapon in _lstWeapons)
            {
                objWeapon.Print(objWriter);
            }
            // </weapons>
            objWriter.WriteEndElement();

            // <cyberwares>
            objWriter.WriteStartElement("cyberwares");
            foreach (Cyberware objCyberware in _lstCyberware)
            {
                objCyberware.Print(objWriter);
            }
            // </cyberwares>
            objWriter.WriteEndElement();

            // Load the Qualities file so we can figure out whether or not each Quality should be printed.
            objXmlDocument = XmlManager.Instance.Load("qualities.xml");

            // <qualities>
            objWriter.WriteStartElement("qualities");
            foreach (Quality objQuality in _lstQualities)
            {
                objQuality.Print(objWriter);
            }
            // </qualities>
            objWriter.WriteEndElement();

            // <lifestyles>
            objWriter.WriteStartElement("lifestyles");
            foreach (Lifestyle objLifestyle in _lstLifestyles)
            {
                objLifestyle.Print(objWriter);
            }
            // </lifestyles>
            objWriter.WriteEndElement();

            // <gears>
            objWriter.WriteStartElement("gears");
            foreach (Gear objGear in _lstGear)
            {
                // Use the Gear's SubClass if applicable.
                // if (objGear.GetType() == typeof(Commlink))
                if (objGear.Category == "Commlinks" || objGear.Category == "Rigger Command Consoles" || objGear.Category == "Cyberdecks" || objGear.GetType() == typeof(Commlink))
                {
                    Commlink objCommlink = new Commlink(this);
                    objCommlink = (Commlink)objGear;
                    objCommlink.Print(objWriter);
                }
                else
                {
                    objGear.Print(objWriter);
                }
            }
            // If the character is a Technomancer, write out the Living Persona "Commlink".
            if (_blnTechnomancerEnabled)
            {
                int intFirewall = _attWIL.TotalValue + _objImprovementManager.ValueOf(Improvement.ImprovementType.LivingPersonaFirewall);
                int intResponse = _attINT.TotalValue + _objImprovementManager.ValueOf(Improvement.ImprovementType.LivingPersonaResponse);
                int intSignal = Convert.ToInt32(Math.Ceiling((Convert.ToDecimal(_attRES.TotalValue, GlobalOptions.Instance.CultureInfo) / 2))) + _objImprovementManager.ValueOf(Improvement.ImprovementType.LivingPersonaSignal);
                int intSystem = _attLOG.TotalValue + _objImprovementManager.ValueOf(Improvement.ImprovementType.LivingPersonaSystem);

                // Make sure none of the Attributes exceed the Technomancer's RES.
                intFirewall = Math.Min(intFirewall, _attRES.TotalValue);
                intResponse = Math.Min(intResponse, _attRES.TotalValue);
                intSignal = Math.Min(intSignal, _attRES.TotalValue);
                intSystem = Math.Min(intSystem, _attRES.TotalValue);

                Commlink objLivingPersona = new Commlink(this);
                objLivingPersona.Name = LanguageManager.Instance.GetString("String_LivingPersona");
                objLivingPersona.Category = LanguageManager.Instance.GetString("String_Commlink");
                objLivingPersona.DeviceRating = _attRES.TotalValue;
                objLivingPersona.Attack = _attCHA.TotalValue;
                objLivingPersona.Sleaze = _attINT.TotalValue;
                objLivingPersona.DataProcessing = _attLOG.TotalValue;
                objLivingPersona.Firewall = _attWIL.TotalValue;
                objLivingPersona.Source = _objOptions.LanguageBookShort("SR5");
                objLivingPersona.Page = "251";
                objLivingPersona.IsLivingPersona = true;

                objLivingPersona.Print(objWriter);
            }
            // </gears>
            objWriter.WriteEndElement();

            // <vehicles>
            objWriter.WriteStartElement("vehicles");
            foreach (Vehicle objVehicle in _lstVehicles)
            {
                objVehicle.Print(objWriter);
            }
            // </vehicles>
            objWriter.WriteEndElement();

            // <metamagics>
            objWriter.WriteStartElement("metamagics");
            foreach (Metamagic objMetamagic in _lstMetamagics)
            {
                objMetamagic.Print(objWriter);
            }
            // </metamagics>
            objWriter.WriteEndElement();

            // <arts>
            objWriter.WriteStartElement("arts");
            foreach (Art objArt in _lstArts)
            {
                objArt.Print(objWriter);
            }
            // </arts>
            objWriter.WriteEndElement();

            // <enhancements>
            objWriter.WriteStartElement("enhancements");
            foreach (Enhancement objEnhancement in _lstEnhancements)
            {
                objEnhancement.Print(objWriter);
            }
            // </enhancements>
            objWriter.WriteEndElement();

            // <critterpowers>
            objWriter.WriteStartElement("critterpowers");
            foreach (CritterPower objPower in _lstCritterPowers)
            {
                objPower.Print(objWriter);
            }
            // </critterpowers>
            objWriter.WriteEndElement();

            // <calendar>
            objWriter.WriteStartElement("calendar");
            //_lstCalendar.Sort();
            foreach (CalendarWeek objWeek in _lstCalendar)
                objWeek.Print(objWriter);
            // </expenses>
            objWriter.WriteEndElement();

            // Print the Expense Log Entries if the option is enabled.
            if (_objOptions.PrintExpenses)
            {
                // <expenses>
                objWriter.WriteStartElement("expenses");
                _lstExpenseLog.Sort(ExpenseLogEntry.CompareDate);
                foreach (ExpenseLogEntry objExpense in _lstExpenseLog)
                    objExpense.Print(objWriter);
                // </expenses>
                objWriter.WriteEndElement();
            }

            // </character>
            objWriter.WriteEndElement();
        }

        /// <summary>
        /// Print this character and open the View Character window.
        /// </summary>
        /// <param name="blnDialog">Whether or not the window should be shown as a dialogue window.</param>
        public void Print(bool blnDialog = true)
        {
            // Write the Character information to a MemoryStream so we don't need to create any files.
            MemoryStream objStream = new MemoryStream();
            XmlTextWriter objWriter = new XmlTextWriter(objStream, Encoding.UTF8);

            // Being the document.
            objWriter.WriteStartDocument();

            // </characters>
            objWriter.WriteStartElement("characters");

            PrintToStream(objStream, objWriter);

            // </characters>
            objWriter.WriteEndElement();

            // Finish the document and flush the Writer and Stream.
            objWriter.WriteEndDocument();
            objWriter.Flush();
            objStream.Flush();

            // Read the stream.
            StreamReader objReader = new StreamReader(objStream);
            objStream.Position = 0;
            XmlDocument objCharacterXML = new XmlDocument();

            // Put the stream into an XmlDocument and send it off to the Viewer.
            string strXML = objReader.ReadToEnd();
            objCharacterXML.LoadXml(strXML);

            objWriter.Close();
            objStream.Close();

            // If a reference to the Viewer window does not yet exist for this character, open a new Viewer window and set the reference to it.
            // If a Viewer window already exists for this character, use it instead.
            if (_frmPrintView == null)
            {
                List<Character> lstCharacters = new List<Character>();
                lstCharacters.Add(this);
                frmViewer frmViewCharacter = new frmViewer();
                frmViewCharacter.Characters = lstCharacters;
                frmViewCharacter.CharacterXML = objCharacterXML;
                _frmPrintView = frmViewCharacter;
                if (blnDialog)
                    frmViewCharacter.ShowDialog();
                else
                    frmViewCharacter.Show();
            }
            else
            {
                _frmPrintView.Activate();
                _frmPrintView.RefreshView();
            }
        }

        /// <summary>
        /// Reset all of the Character information and start from scratch.
        /// </summary>
        private void ResetCharacter()
        {
            _intBuildPoints = 800;
	        _intSumtoTen = 10;
            _intKnowledgeSkills = 0;
            _intKnowledgeSkillPoints = 0;
            _intSkillPoints = 0;
            _intSkillPointsMaximum = 0;
            _intSkillGroups = 0;
            _intSkillGroupsMaximum = 0;
            _decNuyenMaximumBP = 50m;
            _intSpellLimit = 0;
            _intCFPLimit = 0;
            _intContactPoints = 0;
            _intContactPointsUsed = 0;
            _intKarma = 0;
            _intSpecial = 0;
            _intTotalSpecial = 0;
            _intAttributes = 0;
            _intTotalAttributes = 0;
	        _intGameplayOptionQualityLimit = 25;

            // Reset Metatype Information.
            _strMetatype = "";
            _strMetavariant = "";
            _strMetatypeCategory = "";
            _intMetatypeBP = 0;
            _intMutantCritterBaseSkills = 0;
            _strMovement = "";

            // Reset Special Tab Flags.
            _blnAdeptEnabled = false;
            _blnMagicianEnabled = false;
            _blnTechnomancerEnabled = false;
            _blnInitiationEnabled = false;
            _blnCritterEnabled = false;

            // Reset Attributes.
            _attBOD = new Attribute("BOD");
            _attBOD._objCharacter = this;
            _attAGI = new Attribute("AGI");
            _attAGI._objCharacter = this;
            _attREA = new Attribute("REA");
            _attREA._objCharacter = this;
            _attSTR = new Attribute("STR");
            _attSTR._objCharacter = this;
            _attCHA = new Attribute("CHA");
            _attCHA._objCharacter = this;
            _attINT = new Attribute("INT");
            _attINT._objCharacter = this;
            _attLOG = new Attribute("LOG");
            _attLOG._objCharacter = this;
            _attWIL = new Attribute("WIL");
            _attWIL._objCharacter = this;
            _attINI = new Attribute("INI");
            _attINI._objCharacter = this;
            _attEDG = new Attribute("EDG");
            _attEDG._objCharacter = this;
            _attMAG = new Attribute("MAG");
            _attMAG._objCharacter = this;
            _attRES = new Attribute("RES");
            _attRES._objCharacter = this;
            _attESS = new Attribute("ESS");
            _attESS._objCharacter = this;
            _blnMAGEnabled = false;
            _blnRESEnabled = false;
            _blnGroupMember = false;
            _strGroupName = "";
            _strGroupNotes = "";
            _intInitiateGrade = 0;
            _intSubmersionGrade = 0;

            _intMAGAdept = 0;
            _intMAGMagician = 0;
            _strMagicTradition = "";
            _strTechnomancerStream = "";

            // Reset all of the Lists.
            _lstImprovements = new List<Improvement>();
            _lstSkills = new List<Skill>();
            _lstSkillGroups = new List<SkillGroup>();
            _lstContacts = new List<Contact>();
            _lstSpirits = new List<Spirit>();
            _lstSpells = new List<Spell>();
            _lstFoci = new List<Focus>();
            _lstStackedFoci = new List<StackedFocus>();
            _lstPowers = new List<Power>();
            _lstComplexForms = new List<ComplexForm>();
            _lstMartialArts = new List<MartialArt>();
            _lstMartialArtManeuvers = new List<MartialArtManeuver>();
            _lstLimitModifiers = new List<LimitModifier>();
            _lstArmor = new List<Armor>();
            _lstCyberware = new List<Cyberware>();
            _lstMetamagics = new List<Metamagic>();
            _lstArts = new List<Art>();
            _lstEnhancements = new List<Enhancement>();
            _lstWeapons = new List<Weapon>();
            _lstLifestyles = new List<Lifestyle>();
            _lstGear = new List<Gear>();
            _lstVehicles = new List<Vehicle>();
            _lstExpenseLog = new List<ExpenseLogEntry>();
            _lstCritterPowers = new List<CritterPower>();
            _lstInitiationGrades = new List<InitiationGrade>();
            _lstQualities = new List<Quality>();
            _lstOldQualities = new List<string>();
            _lstCalendar = new List<CalendarWeek>();

            BuildSkillList();
            BuildSkillGroupList();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Build the list of Skill Groups.
        /// </summary>
        private void BuildSkillGroupList()
        {
            XmlDocument objXmlDocument = XmlManager.Instance.Load("skills.xml");

            // Populate the Skill Group list.
            XmlNodeList objXmlGroupList = objXmlDocument.SelectNodes("/chummer/skillgroups/name");

            // First pass, build up a list of all of the Skill Groups so we can sort them in alphabetical order for the current language.
            List<ListItem> lstSkillOrder = new List<ListItem>();
            foreach (XmlNode objXmlGroup in objXmlGroupList)
            {
                ListItem objGroup = new ListItem();
                objGroup.Value = objXmlGroup.InnerText;
                if (objXmlGroup.Attributes["translate"] != null)
                    objGroup.Name = objXmlGroup.Attributes["translate"].InnerText;
                else
                    objGroup.Name = objXmlGroup.InnerText;
                lstSkillOrder.Add(objGroup);
            }
            SortListItem objSort = new SortListItem();
            lstSkillOrder.Sort(objSort.Compare);

            // Second pass, retrieve the Skill Groups in the order they're presented in the list.
            foreach (ListItem objItem in lstSkillOrder)
            {
                XmlNode objXmlGroup = objXmlDocument.SelectSingleNode("/chummer/skillgroups/name[. = \"" + objItem.Value + "\"]");
                SkillGroup objGroup = new SkillGroup();
                objGroup.Name = objXmlGroup.InnerText;
                // Maximum Skill Group Rating
                objGroup.RatingMaximum = 6;
                _lstSkillGroups.Add(objGroup);
            }
        }

        /// <summary>
        /// Buid the list of Skills.
        /// </summary>
        private void BuildSkillList()
        {
            // Load the Skills information.
            XmlDocument objXmlDocument = XmlManager.Instance.Load("skills.xml");

            // Populate the Skills list.
            XmlNodeList objXmlSkillList = objXmlDocument.SelectNodes("/chummer/skills/skill[not(exotic) and (" + Options.BookXPath() + ")]");

            // First pass, build up a list of all of the Skills so we can sort them in alphabetical order for the current language.
            List<ListItem> lstSkillOrder = new List<ListItem>();
            foreach (XmlNode objXmlSkill in objXmlSkillList)
            {
                ListItem objSkill = new ListItem();
                objSkill.Value = objXmlSkill["name"].InnerText;
                if (objXmlSkill["translate"] != null)
                    objSkill.Name = objXmlSkill["translate"].InnerText;
                else
                    objSkill.Name = objXmlSkill["name"].InnerText;
                lstSkillOrder.Add(objSkill);
            }
            SortListItem objSort = new SortListItem();
            lstSkillOrder.Sort(objSort.Compare);

            // Second pass, retrieve the Skills in the order they're presented in the list.
            foreach (ListItem objItem in lstSkillOrder)
            {
                XmlNode objXmlSkill = objXmlDocument.SelectSingleNode("/chummer/skills/skill[name = \"" + objItem.Value + "\"]");
                Skill objSkill = new Skill(this);
                objSkill.Name = objXmlSkill["name"].InnerText;
	            objSkill.Id = Guid.Parse(objXmlSkill["id"].InnerText);
                objSkill.SkillCategory = objXmlSkill["category"].InnerText;
                objSkill.SkillGroup = objXmlSkill["skillgroup"].InnerText;
                objSkill.Attribute = objXmlSkill["attribute"].InnerText;
                if (objXmlSkill["default"].InnerText.ToLower() == "yes")
                    objSkill.Default = true;
                else
                    objSkill.Default = false;
                if (objXmlSkill["source"] != null)
                    objSkill.Source = objXmlSkill["source"].InnerText;
                if (objXmlSkill["page"] != null)
                    objSkill.Page = objXmlSkill["page"].InnerText;
                _lstSkills.Add(objSkill);
            }
        }

        /// <summary>
        /// Retrieve the name of the Object that created an Improvement.
        /// </summary>
        /// <param name="objImprovement">Improvement to check.</param>
        public string GetObjectName(Improvement objImprovement)
        {
            string strReturn = "";
            switch (objImprovement.ImproveSource)
            {
                case Improvement.ImprovementSource.Bioware:
                case Improvement.ImprovementSource.Cyberware:
                    foreach (Cyberware objCyberware in _lstCyberware)
                    {
                        if (objCyberware.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objCyberware.DisplayNameShort;
                            break;
                        }
                    }
                    break;
                case Improvement.ImprovementSource.Gear:
                    foreach (Gear objGear in _lstGear)
                    {
                        if (objGear.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objGear.DisplayNameShort;
                            break;
                        }
                        else
                        {
                            foreach (Gear objChild in objGear.Children)
                            {
                                if (objChild.InternalId == objImprovement.SourceName)
                                {
                                    strReturn = objChild.DisplayNameShort;
                                    break;
                                }
                                else
                                {
                                    foreach (Gear objSubChild in objChild.Children)
                                    {
                                        if (objSubChild.InternalId == objImprovement.SourceName)
                                        {
                                            strReturn = objSubChild.DisplayNameShort;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Improvement.ImprovementSource.Spell:
                    foreach (Spell objSpell in _lstSpells)
                    {
                        if (objSpell.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objSpell.DisplayNameShort;
                            break;
                        }
                    }
                    break;
                case Improvement.ImprovementSource.Power:
                    foreach (Power objPower in _lstPowers)
                    {
                        if (objPower.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objPower.DisplayNameShort;
                            break;
                        }
                    }
                    break;
                case Improvement.ImprovementSource.CritterPower:
                    foreach (CritterPower objPower in _lstCritterPowers)
                    {
                        if (objPower.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objPower.DisplayNameShort;
                            break;
                        }
                    }
                    break;
                case Improvement.ImprovementSource.Metamagic:
                case Improvement.ImprovementSource.Echo:
                    foreach (Metamagic objMetamagic in _lstMetamagics)
                    {
                        if (objMetamagic.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objMetamagic.DisplayNameShort;
                            break;
                        }
                    }
                    break;
                case Improvement.ImprovementSource.Art:
                    foreach (Art objArt in _lstArts)
                    {
                        if (objArt.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objArt.DisplayNameShort;
                            break;
                        }
                    }
                    break;
                case Improvement.ImprovementSource.Enhancement:
                    foreach (Enhancement objEnhancement in _lstEnhancements)
                    {
                        if (objEnhancement.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objEnhancement.DisplayNameShort;
                            break;
                        }
                    }
                    break;
                case Improvement.ImprovementSource.Armor:
                    foreach (Armor objArmor in _lstArmor)
                    {
                        if (objArmor.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objArmor.DisplayNameShort;
                            break;
                        }
                    }
                    break;
                case Improvement.ImprovementSource.ArmorMod:
                    foreach (Armor objArmor in _lstArmor)
                    {
                        foreach (ArmorMod objMod in objArmor.ArmorMods)
                        {
                            if (objMod.InternalId == objImprovement.SourceName)
                            {
                                strReturn = objMod.DisplayNameShort;
                                break;
                            }
                        }
                    }
                    break;
                case Improvement.ImprovementSource.ComplexForm:
                    foreach (ComplexForm objProgram in _lstComplexForms)
                    {
                        if (objProgram.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objProgram.DisplayNameShort;
                            break;
                        }
                    }
                    break;
                case Improvement.ImprovementSource.Quality:
                    foreach (Quality objQuality in _lstQualities)
                    {
                        if (objQuality.InternalId == objImprovement.SourceName)
                        {
                            strReturn = objQuality.DisplayNameShort;
                            break;
                        }
                    }
                    break;
                case Improvement.ImprovementSource.MartialArtAdvantage:
                    foreach (MartialArt objMartialArt in _lstMartialArts)
                    {
                        foreach (MartialArtAdvantage objAdvantage in objMartialArt.Advantages)
                        {
                            if (objAdvantage.InternalId == objImprovement.SourceName)
                            {
                                strReturn = objAdvantage.DisplayNameShort;
                                break;
                            }
                        }
                    }
                    break;
                default:
                    if (objImprovement.SourceName == "Armor Encumbrance")
                        strReturn = LanguageManager.Instance.GetString("String_ArmorEncumbrance");
                    else
                    {
                        // If this comes from a custom Improvement, use the name the player gave it instead of showing a GUID.
                        if (objImprovement.CustomName != string.Empty)
                            strReturn = objImprovement.CustomName;
                        else
                            strReturn = objImprovement.SourceName;
                    }
                    break;
            }
            return strReturn;
        }

        /// <summary>
        /// Clean an XPath string.
        /// </summary>
        /// <param name="strValue">String to clean.</param>
        private string CleanXPath(string strValue)
        {
            string strReturn = string.Empty;
            string strSearch = strValue;
            char[] chrQuotes = new char[] { '\'', '"' };

            int intQuotePos = strSearch.IndexOfAny(chrQuotes);
            if (intQuotePos == -1)
            {
                strReturn = "'" + strSearch + "'";
            }
            else
            {
                strReturn = "concat(";
                while (intQuotePos != -1)
                {
                    string strSubstring = strSearch.Substring(0, intQuotePos);
                    strReturn += "'" + strSubstring + "', ";
                    if (strSearch.Substring(intQuotePos, 1) == "'")
                    {
                        strReturn += "\"'\", ";
                    }
                    else
                    {
                        //must be a double quote
                        strReturn += "'\"', ";
                    }
                    strSearch = strSearch.Substring(intQuotePos + 1, strSearch.Length - intQuotePos - 1);
                    intQuotePos = strSearch.IndexOfAny(chrQuotes);
                }
                strReturn += "'" + strSearch + "')";
            }
            return strReturn;

        }
        #endregion

        #region Basic Properties
        /// <summary>
        /// Character Options object.
        /// </summary>
        public CharacterOptions Options
        {
            get
            {
                return _objOptions;
            }
        }

        /// <summary>
        /// Name of the file the Character is saved to.
        /// </summary>
        public string FileName
        {
            get
            {
                return _strFileName;
            }
            set
            {
                _strFileName = value;
            }
        }

        /// <summary>
        /// Name of the settings file the Character uses. 
        /// </summary>
        public string SettingsFile
        {
            get
            {
                return _strSettingsFileName;
            }
            set
            {
                _strSettingsFileName = value;
                _objOptions.Load(_strSettingsFileName);
            }
        }

        /// <summary>
        /// Whether or not the character has been saved as Created and can no longer be modified using the Build system.
        /// </summary>
        public bool Created
        {
            get
            {
                return _blnCreated;
            }
            set
            {
                _blnCreated = value;
            }
        }

        /// <summary>
        /// Character's name.
        /// </summary>
        public string Name
        {
            get
            {
                return _strName;
            }
            set
            {
                _strName = value;
                try
                {
                    CharacterNameChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Character's portrait encoded using Base64.
        /// </summary>
        public string Mugshot
        {
            get
            {
                return _strMugshot;
            }
            set
            {
                _strMugshot = value;
            }
        }

        /// <summary>
        /// Character's Gameplay Option.
        /// </summary>
        public string GameplayOption
        {
            get
            {
                return _strGameplayOption;
            }
            set
            {
                _strGameplayOption = value;
            }
        }

		/// <summary>
		/// Quality Limit conferred by the Character's Gameplay Option
		/// </summary>
		public int GameplayOptionQualityLimit
		{
			get
			{
				return _intGameplayOptionQualityLimit;
			}
			set
			{
				_intGameplayOptionQualityLimit = value;
			}
		}

		/// <summary>
		/// Character's maximum karma at character creation.
		/// </summary>
		public int MaxKarma
        {
            get
            {
                return _intMaxKarma;
            }
            set
            {
                _intMaxKarma = value;
            }
        }

        /// <summary>
        /// Character's maximum nuyen at character creation.
        /// </summary>
        public int MaxNuyen
        {
            get
            {
                return _intMaxNuyen;
            }
            set
            {
                _intMaxNuyen = value;
            }
        }

        /// <summary>
        /// Character's contact point multiplier.
        /// </summary>
        public int ContactMultiplier
        {
            get
            {
                return _intContactMultiplier;
            }
            set
            {
                _intContactMultiplier = value;
            }
        }

        /// <summary>
        /// Character's Metatype Priority.
        /// </summary>
        public string MetatypePriority
        {
            get
            {
                return _strPriorityMetatype;
            }
            set
            {
                _strPriorityMetatype = value;
            }
        }

        /// <summary>
        /// Character's Attributes Priority.
        /// </summary>
        public string AttributesPriority
        {
            get
            {
                return _strPriorityAttributes;
            }
            set
            {
                _strPriorityAttributes = value;
            }
        }

        /// <summary>
        /// Character's Special Priority.
        /// </summary>
        public string SpecialPriority
        {
            get
            {
                return _strPrioritySpecial;
            }
            set
            {
                _strPrioritySpecial = value;
            }
        }

        /// <summary>
        /// Character's Skills Priority.
        /// </summary>
        public string SkillsPriority
        {
            get
            {
                return _strPrioritySkills;
            }
            set
            {
                _strPrioritySkills = value;
            }
        }

        /// <summary>
        /// Character's Resources Priority.
        /// </summary>
        public string ResourcesPriority
        {
            get
            {
                return _strPriorityResources;
            }
            set
            {
                _strPriorityResources = value;
            }
        }

        /// <summary>
        /// Character's 1st bonus skill.
        /// </summary>
        public string PriorityBonusSkill1
        {
            get
            {
                return _strSkill1;
            }
            set
            {
                _strSkill1 = value;
            }
        }

        /// <summary>
        /// Character's 2nd bonus skill.
        /// </summary>
        public string PriorityBonusSkill2
        {
            get
            {
                return _strSkill2;
            }
            set
            {
                _strSkill2 = value;
            }
        }

        /// <summary>
        /// Character's bonus skill group.
        /// </summary>
        public string PriorityBonusSkillGroup
        {
            get
            {
                return _strSkillGroup;
            }
            set
            {
                _strSkillGroup = value;
            }
        }

        /// <summary>
        /// Character's sex.
        /// </summary>
        public string Sex
        {
            get
            {
                return _strSex;
            }
            set
            {
                _strSex = value;
            }
        }

        /// <summary>
        /// Character's age.
        /// </summary>
        public string Age
        {
            get
            {
                return _strAge;
            }
            set
            {
                _strAge = value;
            }
        }

        /// <summary>
        /// Character's eyes.
        /// </summary>
        public string Eyes
        {
            get
            {
                return _strEyes;
            }
            set
            {
                _strEyes = value;
            }
        }

        /// <summary>
        /// Character's height.
        /// </summary>
        public string Height
        {
            get
            {
                return _strHeight;
            }
            set
            {
                _strHeight = value;
            }
        }

        /// <summary>
        /// Character's weight.
        /// </summary>
        public string Weight
        {
            get
            {
                return _strWeight;
            }
            set
            {
                _strWeight = value;
            }
        }

        /// <summary>
        /// Character's skin.
        /// </summary>
        public string Skin
        {
            get
            {
                return _strSkin;
            }
            set
            {
                _strSkin = value;
            }
        }

        /// <summary>
        /// Character's hair.
        /// </summary>
        public string Hair
        {
            get
            {
                return _strHair;
            }
            set
            {
                _strHair = value;
            }
        }

        /// <summary>
        /// Character's description.
        /// </summary>
        public string Description
        {
            get
            {
                return _strDescription;
            }
            set
            {
                _strDescription = value;
            }
        }

        /// <summary>
        /// Character's background.
        /// </summary>
        public string Background
        {
            get
            {
                return _strBackground;
            }
            set
            {
                _strBackground = value;
            }
        }

        /// <summary>
        /// Character's concept.
        /// </summary>
        public string Concept
        {
            get
            {
                return _strConcept;
            }
            set
            {
                _strConcept = value;
            }
        }

        /// <summary>
        /// Character notes.
        /// </summary>
        public string Notes
        {
            get
            {
                return _strNotes;
            }
            set
            {
                _strNotes = value;
            }
        }

        /// <summary>
        /// General gameplay notes.
        /// </summary>
        public string GameNotes
        {
            get
            {
                return _strGameNotes;
            }
            set
            {
                _strGameNotes = value;
            }
        }

        /// <summary>
        /// Player name.
        /// </summary>
        public string PlayerName
        {
            get
            {
                return _strPlayerName;
            }
            set
            {
                _strPlayerName = value;
            }
        }

        /// <summary>
        /// Character's alias.
        /// </summary>
        public string Alias
        {
            get
            {
                return _strAlias;
            }
            set
            {
                _strAlias = value;
                try
                {
                    CharacterNameChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Street Cred.
        /// </summary>
        public int StreetCred
        {
            get
            {
                return _intStreetCred;
            }
            set
            {
                _intStreetCred = value;
            }
        }

        /// <summary>
        /// Burnt Street Cred.
        /// </summary>
        public int BurntStreetCred
        {
            get
            {
                return _intBurntStreetCred;
            }
            set
            {
                _intBurntStreetCred = value;
            }
        }

        /// <summary>
        /// Notoriety.
        /// </summary>
        public int Notoriety
        {
            get
            {
                return _intNotoriety;
            }
            set
            {
                _intNotoriety = value;
            }
        }

        /// <summary>
        /// Public Awareness.
        /// </summary>
        public int PublicAwareness
        {
            get
            {
                return _intPublicAwareness;
            }
            set
            {
                _intPublicAwareness = value;
            }
        }

        /// <summary>
        /// Number of Physical Condition Monitor Boxes that are filled.
        /// </summary>
        public int PhysicalCMFilled
        {
            get
            {
                return _intPhysicalCMFilled;
            }
            set
            {
                _intPhysicalCMFilled = value;
            }
        }

        /// <summary>
        /// Number of Stun Condition Monitor Boxes that are filled.
        /// </summary>
        public int StunCMFilled
        {
            get
            {
                return _intStunCMFilled;
            }
            set
            {
                _intStunCMFilled = value;
            }
        }

        /// <summary>
        /// Whether or not character creation rules should be ignored.
        /// </summary>
        public bool IgnoreRules
        {
            get
            {
                return _blnIgnoreRules;
            }
            set
            {
                _blnIgnoreRules = value;
            }
        }

        /// <summary>
        /// Contact Points.
        /// </summary>
        public int ContactPoints
        {
            get
            {
                return _intContactPoints;
            }
            set
            {
                _intContactPoints = value;
            }
        }


        /// <summary>
        /// Number of free Contact Points the character has used.
        /// </summary>
        public int ContactPointsUsed
        {
            get
            {
                return _intContactPointsUsed;
            }
            set
            {
                _intContactPointsUsed = value;
            }
        }

        /// <summary>
        /// CFP Limit.
        /// </summary>
        public int CFPLimit
        {
            get
            {
                return _intCFPLimit;
            }
            set
            {
                _intCFPLimit = value;
            }
        }

        /// <summary>
        /// Spell Limit.
        /// </summary>
        public int SpellLimit
        {
            get
            {
                return _intSpellLimit;
            }
            set
            {
                _intSpellLimit = value;
            }
        }

        /// <summary>
        /// Karma.
        /// </summary>
        public int Karma
        {
            get
            {
                return _intKarma;
            }
            set
            {
                _intKarma = value;
            }
        }

        /// <summary>
        /// Special.
        /// </summary>
        public int Special
        {
            get
            {
                return _intSpecial;
            }
            set
            {
                _intSpecial = value;
            }
        }

        /// <summary>
        /// TotalSpecial.
        /// </summary>
        public int TotalSpecial
        {
            get
            {
                return _intTotalSpecial;
            }
            set
            {
                _intTotalSpecial = value;
            }
        }

        /// <summary>
        /// Attributes.
        /// </summary>
        public int Attributes
        {
            get
            {
                return _intAttributes;
            }
            set
            {
                _intAttributes = value;
            }
        }

        /// <summary>
        /// TotalAttributes.
        /// </summary>
        public int TotalAttributes
        {
            get
            {
                return _intTotalAttributes;
            }
            set
            {
                _intTotalAttributes = value;
            }
        }

        /// <summary>
        /// Total amount of Karma the character has earned over the career.
        /// </summary>
        public int CareerKarma
        {
            get
            {
                int intKarma = 0;

                foreach (ExpenseLogEntry objEntry in _lstExpenseLog)
                {
                    // Since we're only interested in the amount they have earned, only count values that are greater than 0 and are not refunds.
                    if (objEntry.Type == ExpenseType.Karma && objEntry.Amount > 0 && objEntry.Refund == false)
                        intKarma += objEntry.Amount;
                }

                return intKarma;
            }
        }

        /// <summary>
        /// Total amount of Nuyen the character has earned over the career.
        /// </summary>
        public int CareerNuyen
        {
            get
            {
                int intNuyen = 0;

                foreach (ExpenseLogEntry objEntry in _lstExpenseLog)
                {
                    // Since we're only interested in the amount they have earned, only count values that are greater than 0 and are not refunds.
                    if (objEntry.Type == ExpenseType.Nuyen && objEntry.Amount > 0 && objEntry.Refund == false)
                        intNuyen += objEntry.Amount;
                }

                return intNuyen;
            }
        }

        /// <summary>
        /// Whether or not the character is a Critter.
        /// </summary>
        public bool IsCritter
        {
            get
            {
                return _blnIsCritter;
            }
            set
            {
                _blnIsCritter = value;
            }
        }

        /// <summary>
        /// Whether or not the character is possessed by a Spirit.
        /// </summary>
        public bool Possessed
        {
            get
            {
                return _blnPossessed;
            }
            set
            {
                _blnPossessed = value;
            }
        }

        /// <summary>
        /// Whether or not we should override the option of how Special Attribute Essence Loss is handled. When enabled, ESS loss always affects the character's maximum MAG/RES instead.
        /// This should only be enabled as a result of swapping out a Latent Quality for its fully-realised version.
        /// </summary>
        public bool OverrideSpecialAttributeEssenceLoss
        {
            get
            {
                return _blnOverrideSpecialAttributeESSLoss;
            }
            set
            {
                _blnOverrideSpecialAttributeESSLoss = value;
            }
        }

        /// <summary>
        /// Maximum item Availability for new characters.
        /// </summary>
        public int MaximumAvailability
        {
            get
            {
                return _intMaxAvail;
            }
            set
            {
                _intMaxAvail = value;
            }
        }
        #endregion

        #region Attributes
        /// <summary>
        /// Get an Attribute by its name.
        /// </summary>
        /// <param name="strAttribute">Attribute name to retrieve.</param>
        public Attribute GetAttribute(string strAttribute)
        {
            switch (strAttribute)
            {
                case "BOD":
                case "BODBase":
                    return _attBOD;
                case "AGI":
                case "AGIBase":
                    return _attAGI;
                case "REA":
                case "REABase":
                    return _attREA;
                case "STR":
                case "STRBase":
                    return _attSTR;
                case "CHA":
                case "CHABase":
                    return _attCHA;
                case "INT":
                case "INTBase":
                    return _attINT;
                case "LOG":
                case "LOGBase":
                    return _attLOG;
                case "WIL":
                case "WILBase":
                    return _attWIL;
                case "INI":
                    return _attINI;
                case "EDG":
                case "EDGBase":
                    return _attEDG;
                case "MAG":
                case "MAGBase":
                    return _attMAG;
                case "RES":
                case "RESBase":
                    return _attRES;
                case "ESS":
                    return _attESS;
                default:
                    return _attBOD;
            }
        }

        /// <summary>
        /// Body (BOD) Attribute.
        /// </summary>
        public Attribute BOD
        {
            get
            {
                return _attBOD;
            }
        }

        /// <summary>
        /// Agility (AGI) Attribute.
        /// </summary>
        public Attribute AGI
        {
            get
            {
                return _attAGI;
            }
        }

        /// <summary>
        /// Reaction (REA) Attribute.
        /// </summary>
        public Attribute REA
        {
            get
            {
                return _attREA;
            }
        }

        /// <summary>
        /// Strength (STR) Attribute.
        /// </summary>
        public Attribute STR
        {
            get
            {
                return _attSTR;
            }
        }

        /// <summary>
        /// Charisma (CHA) Attribute.
        /// </summary>
        public Attribute CHA
        {
            get
            {
                return _attCHA;
            }
        }

        /// <summary>
        /// Intuition (INT) Attribute.
        /// </summary>
        public Attribute INT
        {
            get
            {
                return _attINT;
            }
        }

        /// <summary>
        /// Logic (LOG) Attribute.
        /// </summary>
        public Attribute LOG
        {
            get
            {
                return _attLOG;
            }
        }

        /// <summary>
        /// Willpower (WIL) Attribute.
        /// </summary>
        public Attribute WIL
        {
            get
            {
                return _attWIL;
            }
        }

        /// <summary>
        /// Initiative (INI) Attribute.
        /// </summary>
        public Attribute INI
        {
            get
            {
                return _attINI;
            }
        }

        /// <summary>
        /// Edge (EDG) Attribute.
        /// </summary>
        public Attribute EDG
        {
            get
            {
                return _attEDG;
            }
        }

        /// <summary>
        /// Magic (MAG) Attribute.
        /// </summary>
        public Attribute MAG
        {
            get
            {
                return _attMAG;
            }
        }

        /// <summary>
        /// Resonance (RES) Attribute.
        /// </summary>
        public Attribute RES
        {
            get
            {
                return _attRES;
            }
        }

        /// <summary>
        /// Essence (ESS) Attribute.
        /// </summary>
        public Attribute ESS
        {
            get
            {
                return _attESS;
            }
        }

        /// <summary>
        /// Is the MAG Attribute enabled?
        /// </summary>
        public bool MAGEnabled
        {
            get
            {
                return _blnMAGEnabled;
            }
            set
            {
                bool blnOldValue = _blnMAGEnabled;
                _blnMAGEnabled = value;
                if (value && Created)
                    _decEssenceAtSpecialStart = Essence;
                try
                {
                    if (blnOldValue != value)
                        MAGEnabledChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Amount of MAG invested in Adept for Mystic Adepts.
        /// </summary>
        public int MAGAdept
        {
            get
            {
                return _intMAGAdept;
            }
            set
            {
                _intMAGAdept = value;
            }
        }

        /// <summary>
        /// Amount of MAG invested in Magician for Mystic Adepts.
        /// </summary>
        public int MAGMagician
        {
            get
            {
                return _intMAGMagician;
            }
            set
            {
                _intMAGMagician = value;
            }
        }

        /// <summary>
        /// Magician's Tradition.
        /// </summary>
        public string MagicTradition
        {
            get
            {
                return _strMagicTradition;
            }
            set
            {
                _strMagicTradition = value;
            }
        }

        /// <summary>
        /// Magician's Tradition Drain Attributes.
        /// </summary>
        public string TraditionDrain
        {
            get
            {
                return _strTraditionDrain;
            }
            set
            {
                _strTraditionDrain = value;
            }
        }

        /// <summary>
        /// Magician's Tradition Name (for Custom Traditions).
        /// </summary>
        public string TraditionName
        {
            get
            {
                return _strTraditionName;
            }
            set
            {
                _strTraditionName = value;
            }
        }

        /// <summary>
        /// Magician's Combat Spirit (for Custom Traditions).
        /// </summary>
        public string SpiritCombat
        {
            get
            {
                return _strSpiritCombat;
            }
            set
            {
                _strSpiritCombat = value;
            }
        }

        /// <summary>
        /// Magician's Detection Spirit (for Custom Traditions).
        /// </summary>
        public string SpiritDetection
        {
            get
            {
                return _strSpiritDetection;
            }
            set
            {
                _strSpiritDetection = value;
            }
        }

        /// <summary>
        /// Magician's Health Spirit (for Custom Traditions).
        /// </summary>
        public string SpiritHealth
        {
            get
            {
                return _strSpiritHealth;
            }
            set
            {
                _strSpiritHealth = value;
            }
        }

        /// <summary>
        /// Magician's Illusion Spirit (for Custom Traditions).
        /// </summary>
        public string SpiritIllusion
        {
            get
            {
                return _strSpiritIllusion;
            }
            set
            {
                _strSpiritIllusion = value;
            }
        }

        /// <summary>
        /// Magician's Manipulation Spirit (for Custom Traditions).
        /// </summary>
        public string SpiritManipulation
        {
            get
            {
                return _strSpiritManipulation;
            }
            set
            {
                _strSpiritManipulation = value;
            }
        }

        /// <summary>
        /// Technomancer's Stream.
        /// </summary>
        public string TechnomancerStream
        {
            get
            {
                return _strTechnomancerStream;
            }
            set
            {
                _strTechnomancerStream = value;
            }
        }

        /// <summary>
        /// Initiate Grade.
        /// </summary>
        public int InitiateGrade
        {
            get
            {
                return _intInitiateGrade;
            }
            set
            {
                _intInitiateGrade = value;
            }
        }

        /// <summary>
        /// Is the RES Attribute enabled?
        /// </summary>
        public bool RESEnabled
        {
            get
            {
                return _blnRESEnabled;
            }
            set
            {
                bool blnOldValue = _blnRESEnabled;
                _blnRESEnabled = value;
                if (value && Created)
                    _decEssenceAtSpecialStart = Essence;
                try
                {
                    if (blnOldValue != value)
                        RESEnabledChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Submersion Grade.
        /// </summary>
        public int SubmersionGrade
        {
            get
            {
                return _intSubmersionGrade;
            }
            set
            {
                _intSubmersionGrade = value;
            }
        }

        /// <summary>
        /// Whether or not the character is a member of a Group or Network.
        /// </summary>
        public bool GroupMember
        {
            get
            {
                return _blnGroupMember;
            }
            set
            {
                _blnGroupMember = value;
            }
        }

        /// <summary>
        /// The name of the Group the Initiate has joined.
        /// </summary>
        public string GroupName
        {
            get
            {
                return _strGroupName;
            }
            set
            {
                _strGroupName = value;
            }
        }

        /// <summary>
        /// Notes for the Group the Initiate has joined.
        /// </summary>
        public string GroupNotes
        {
            get
            {
                return _strGroupNotes;
            }
            set
            {
                _strGroupNotes = value;
            }
        }

        /// <summary>
        /// Essence the character had when the first gained access to MAG/RES.
        /// </summary>
        public decimal EssenceAtSpecialStart
        {
            get
            {
                return _decEssenceAtSpecialStart;
            }
        }

        /// <summary>
        /// Character's Essence.
        /// </summary>
        public decimal Essence
        {
            get
            {
                decimal decESS = Convert.ToDecimal(_attESS.MetatypeMaximum, GlobalOptions.Instance.CultureInfo) + Convert.ToDecimal(_objImprovementManager.ValueOf(Improvement.ImprovementType.Essence), GlobalOptions.Instance.CultureInfo);
                // Run through all of the pieces of Cyberware and include their Essence cost. Cyberware and Bioware costs are calculated separately. The higher value removes its full cost from the
                // character's ESS while the lower removes half of its cost from the character's ESS.
                decimal decCyberware = 0m;
                decimal decBioware = 0m;
                decimal decHole = 0m;
                foreach (Cyberware objCyberware in _lstCyberware)
                {
                    if (objCyberware.Name == "Essence Hole")
                        decHole += objCyberware.CalculatedESS;
                    else
                    {
                        if (objCyberware.SourceType == Improvement.ImprovementSource.Cyberware)
                            decCyberware += objCyberware.CalculatedESS;
                        else if (objCyberware.SourceType == Improvement.ImprovementSource.Bioware)
                            decBioware += objCyberware.CalculatedESS;
                    }
                }
                decESS -= decCyberware + decBioware;
                // Deduct the Essence Hole value.
                decESS -= decHole;

                // If the character has a fixed Essence Improvement, permanently fix their Essence at its value.
                foreach (Improvement objImprovement in _lstImprovements)
                {
                    if (objImprovement.ImproveType == Improvement.ImprovementType.CyborgEssence && objImprovement.Enabled)
                    {
                        decESS = 0.1m;
                        break;
                    }
                }

                return decESS;
            }
        }

        /// <summary>
        /// Essence consumed by Cyberware.
        /// </summary>
        public decimal CyberwareEssence
        {
            get
            {
                decimal decESS = Convert.ToDecimal(_attESS.MetatypeMaximum, GlobalOptions.Instance.CultureInfo) + Convert.ToDecimal(_objImprovementManager.ValueOf(Improvement.ImprovementType.Essence), GlobalOptions.Instance.CultureInfo);
                // Run through all of the pieces of Cyberware and include their Essence cost. Cyberware and Bioware costs are calculated separately. The higher value removes its full cost from the
                // character's ESS while the lower removes half of its cost from the character's ESS.
                decimal decCyberware = 0m;
                decimal decBioware = 0m;
                decimal decHole = 0m;
                foreach (Cyberware objCyberware in _lstCyberware)
                {
                    if (objCyberware.Name == "Essence Hole")
                        decHole += objCyberware.CalculatedESS;
                    else
                    {
                        if (objCyberware.SourceType == Improvement.ImprovementSource.Cyberware)
                            decCyberware += objCyberware.CalculatedESS;
                        else if (objCyberware.SourceType == Improvement.ImprovementSource.Bioware)
                            decBioware += objCyberware.CalculatedESS;
                    }
                }
                // Removed Cyber/Bio discount
                //if (decCyberware > decBioware)
                return decCyberware;
                //else
                //    return decCyberware / 2;
            }
        }

        /// <summary>
        /// Essence consumed by Bioware.
        /// </summary>
        public decimal BiowareEssence
        {
            get
            {
                decimal decESS = Convert.ToDecimal(_attESS.MetatypeMaximum, GlobalOptions.Instance.CultureInfo) + Convert.ToDecimal(_objImprovementManager.ValueOf(Improvement.ImprovementType.Essence), GlobalOptions.Instance.CultureInfo);
                // Run through all of the pieces of Cyberware and include their Essence cost. Cyberware and Bioware costs are calculated separately. The higher value removes its full cost from the
                // character's ESS while the lower removes half of its cost from the character's ESS.
                decimal decCyberware = 0m;
                decimal decBioware = 0m;
                decimal decHole = 0m;
                foreach (Cyberware objCyberware in _lstCyberware)
                {
                    if (objCyberware.Name == "Essence Hole")
                        decHole += objCyberware.CalculatedESS;
                    else
                    {
                        if (objCyberware.SourceType == Improvement.ImprovementSource.Cyberware)
                            decCyberware += objCyberware.CalculatedESS;
                        else if (objCyberware.SourceType == Improvement.ImprovementSource.Bioware)
                            decBioware += objCyberware.CalculatedESS;
                    }
                }
                // Removed Cyber/Bio discount
                //if (decCyberware > decBioware)
                //  return decBioware / 2;
                //else
                return decBioware;
            }
        }

        /// <summary>
        /// Essence consumed by Essence Holes.
        /// </summary>
        public decimal EssenceHole
        {
            get
            {
                decimal decESS = Convert.ToDecimal(_attESS.MetatypeMaximum, GlobalOptions.Instance.CultureInfo) + Convert.ToDecimal(_objImprovementManager.ValueOf(Improvement.ImprovementType.Essence), GlobalOptions.Instance.CultureInfo);
                // Run through all of the pieces of Cyberware and include their Essence cost. Cyberware and Bioware costs are calculated separately. The higher value removes its full cost from the
                // character's ESS while the lower removes half of its cost from the character's ESS.
                decimal decCyberware = 0m;
                decimal decBioware = 0m;
                decimal decHole = 0m;
                foreach (Cyberware objCyberware in _lstCyberware)
                {
                    if (objCyberware.Name == "Essence Hole")
                        decHole += objCyberware.CalculatedESS;
                    else
                    {
                        if (objCyberware.SourceType == Improvement.ImprovementSource.Cyberware)
                            decCyberware += objCyberware.CalculatedESS;
                        else if (objCyberware.SourceType == Improvement.ImprovementSource.Bioware)
                            decBioware += objCyberware.CalculatedESS;
                    }
                }

                return decHole;
            }
        }

        /// <summary>
        /// Character's maximum Essence.
        /// </summary>
        public decimal EssenceMaximum
        {
            get
            {
                decimal decESS = Convert.ToDecimal(_attESS.MetatypeMaximum, GlobalOptions.Instance.CultureInfo) + Convert.ToDecimal(_objImprovementManager.ValueOf(Improvement.ImprovementType.EssenceMax), GlobalOptions.Instance.CultureInfo);
                return decESS;
            }
        }

        /// <summary>
        /// Character's total Essence Loss penalty.
        /// </summary>
        public int EssencePenalty
        {
            get
            {
                int intReturn = 0;
                // Subtract the character's current Essence from its maximum. Round the remaining amount up to get the total penalty to MAG and RES.
                intReturn = Convert.ToInt32(Math.Ceiling(EssenceAtSpecialStart + _objImprovementManager.ValueOf(Improvement.ImprovementType.EssenceMax) - Essence));

                return intReturn;
            }
        }

        /// <summary>
        /// Initiative.
        /// </summary>
        public string Initiative
        {
            get
            {
                string strReturn = "";

                // Start by adding INT and REA together.
                int intINI = _attINT.TotalValue + _attREA.TotalValue;
                // Add modifiers.
                intINI += _attINI.AttributeModifiers;
                // Add in any Initiative Improvements.
                intINI += _objImprovementManager.ValueOf(Improvement.ImprovementType.Initiative) + WoundModifiers;

                // If INI exceeds the Metatype maximum set it back to the maximum.
                //if (intINI > _attINI.MetatypeAugmentedMaximum)
                //    intINI = _attINI.MetatypeAugmentedMaximum;
                if (intINI < 0)
                    intINI = 0;
                if (_attINT.Value + _attREA.Value != intINI)
                    strReturn = (_attINT.Value + _attREA.Value).ToString() + " (" + intINI.ToString() + ")";
                else
                strReturn = (intINI).ToString();

                int intExtraIP = 1 + Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.InitiativePass)) + Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.InitiativePassAdd));
                strReturn += " + " + intExtraIP.ToString() + "d6";

                return strReturn;
            }
        }

        /// <summary>
        /// Initiative Passes.
        /// </summary>
        public string InitiativePasses
        {
            get
            {
                string strReturn = "";

                int intExtraIP = 1 + Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.InitiativePass)) + Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.InitiativePassAdd));
                //if (intIP != intExtraIP)
                //    strReturn = "1 (" + intExtraIP.ToString() + ")";
                //else
                strReturn = intExtraIP.ToString();

                return strReturn;
            }
        }

        /// <summary>
        /// Astral Initiative.
        /// </summary>
        public string AstralInitiative
        {
            get
            {
                string strReturn = "";

                int intINI = (_attINT.TotalValue * 2) + WoundModifiers;
                if (intINI < 0)
                    intINI = 0;
                strReturn = (_attINT.TotalValue * 2).ToString();
                //if (intINI != _attINT.TotalValue * 2)
                //    strReturn += " (" + intINI.ToString() + ")";

                int intExtraIP = 2;
                strReturn += " + " + intExtraIP.ToString() + "d6";

                return strReturn;
            }
        }

        /// <summary>
        /// Astral Initiative Passes.
        /// </summary>
        public string AstralInitiativePasses
        {
            get
            {
                return "3";
            }
        }

        /// <summary>
        /// Matrix Initiative.
        /// </summary>
        public string MatrixInitiative
        {
            get
            {
                string strReturn = "";
                //int intMatrixInit = 0;

                //// This is always calculated since characters can have a Matrix Initiative without actually being a Technomancer.
                //if (!TechnomancerEnabled)
                //{
                //    intMatrixInit = _attINT.TotalValue;
                //    int intCommlinkResponse = 0;

                //    // Retrieve the Response for the character's active Commlink.
                //    foreach (Commlink objCommlink in _lstGear.OfType<Commlink>())
                //    {
                //        if (objCommlink.IsActive)
                //            intCommlinkResponse = objCommlink.TotalResponse;
                //    }
                //    intMatrixInit += intCommlinkResponse;

                //    // Add in any Matrix Initiative Improvements.
                //    intMatrixInit += _objImprovementManager.ValueOf(Improvement.ImprovementType.MatrixInitiative);
                //}
                //else
                //{
                //    // Technomancer Matrix Initiative = INT * 2 + 1 + any Living Persona bonuses.
                //    intMatrixInit = (_attINT.TotalValue * 2) + 1 + _objImprovementManager.ValueOf(Improvement.ImprovementType.LivingPersonaResponse);
                //}

                //// Sprites have a forced value, so use that instead.
                //if (_strMetatype.EndsWith("Sprite"))
                //    intMatrixInit = _attINI.MetatypeMinimum;
                //// A.I.s caculate their totals differently. (INT + Response)
                //if (_strMetatype.EndsWith("A.I.") || _strMetatypeCategory == "Technocritters" || _strMetatypeCategory == "Protosapients")
                //    intMatrixInit = (_attINT.TotalValue + Response);

                //int intINI = intMatrixInit + WoundModifiers;
                //if (intINI < 0)
                //    intINI = 0;

                //strReturn = intMatrixInit.ToString();
                //if (intINI != intMatrixInit)
                //    strReturn += " (" + intINI.ToString() + ")";

                int intINI = (_attINT.TotalValue + _attREA.TotalValue) + WoundModifiers;
                intINI += _objImprovementManager.ValueOf(Improvement.ImprovementType.Initiative) + WoundModifiers;
                if (intINI < 0)
                    intINI = 0;
                strReturn = (intINI).ToString();

                int intExtraIP = 1 + Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.InitiativePass)) + Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.InitiativePassAdd));
                strReturn += " + " + intExtraIP.ToString() + "d6";

                return strReturn;
            }
        }

        /// <summary>
        /// Matrix Initiative Passes.
        /// </summary>
        public string MatrixInitiativePasses
        {
            get
            {
                string strReturn = "";
                int intIP = 0;

                if (!TechnomancerEnabled)
                {
                    // Standard characters get 1 IP + any Matrix Initiative Pass bonuses.
                    intIP = 1 + _objImprovementManager.ValueOf(Improvement.ImprovementType.MatrixInitiativePass);
                }
                else
                {
                    // Techomancers get 3 IPs + any Matrix Initiative Pass bonuses.
                    intIP = 3 + _objImprovementManager.ValueOf(Improvement.ImprovementType.MatrixInitiativePass);
                }

                // A.I.s always have 3 Matrix Initiative Passes.
                if (_strMetatype.EndsWith("A.I.") || _strMetatypeCategory == "Technocritters" || _strMetatypeCategory == "Protosapients")
                    intIP = 3;

                // Add in any additional Matrix Initiative Pass bonuses.
                intIP += _objImprovementManager.ValueOf(Improvement.ImprovementType.MatrixInitiativePassAdd);

                strReturn = intIP.ToString();

                return strReturn;
            }
        }

        /// <summary>
        /// Matrix Initiative via VR with Cold Sim.
        /// </summary>
        public string MatrixInitiativeCold
        {
            get
            {
                string strReturn = "";

                int intINI = (_attINT.TotalValue) + WoundModifiers;
                if (intINI < 0)
                    intINI = 0;
                strReturn = (_attINT.TotalValue).ToString();

                int intExtraIP = 3;
                strReturn += " + DP + " + intExtraIP.ToString() + "d6";

                return strReturn;
            }
        }

        /// <summary>
        /// Matrix Initiative via VR with Hot Sim.
        /// </summary>
        public string MatrixInitiativeHot
        {
            get
            {
                string strReturn = "";

                int intINI = (_attINT.TotalValue) + WoundModifiers;
                if (intINI < 0)
                    intINI = 0;
                strReturn = (_attINT.TotalValue).ToString();

                int intExtraIP = 4;
                strReturn += " + DP + " + intExtraIP.ToString() + "d6";

                return strReturn;
            }
        }

        /// <summary>
        /// Rigger Initiative
        /// </summary>
        public string RiggerInitiative
        {
            get
            {
                string strReturn = "";

                int intINI = (_attINT.TotalValue + _attREA.TotalValue) + WoundModifiers;
                intINI += _objImprovementManager.ValueOf(Improvement.ImprovementType.Initiative) + WoundModifiers;
                if (intINI < 0)
                    intINI = 0;
                strReturn = (intINI).ToString();

                int intExtraIP = 1 + Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.InitiativePass)) + Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.InitiativePassAdd));
                strReturn += " + " + intExtraIP.ToString() + "d6";

                return strReturn;
            }
        }

        /// <summary>
        /// An A.I.'s Rating.
        /// </summary>
        public int Rating
        {
            get
            {
                double dblAverage = Convert.ToDouble(_attCHA.TotalValue, GlobalOptions.Instance.CultureInfo) + Convert.ToDouble(_attINT.TotalValue, GlobalOptions.Instance.CultureInfo) + Convert.ToDouble(_attLOG.TotalValue, GlobalOptions.Instance.CultureInfo) + Convert.ToDouble(_attWIL.TotalValue, GlobalOptions.Instance.CultureInfo);
                dblAverage = Math.Ceiling(dblAverage / 4);
                return Convert.ToInt32(dblAverage);
            }
        }

        /// <summary>
        /// An A.I.'s System.
        /// </summary>
        public int System
        {
            get
            {
                double dblAverage = Convert.ToDouble(_attINT.TotalValue, GlobalOptions.Instance.CultureInfo) + Convert.ToDouble(_attLOG.TotalValue, GlobalOptions.Instance.CultureInfo);
                dblAverage = Math.Ceiling(dblAverage / 2);
                return Convert.ToInt32(dblAverage);
            }
        }

        /// <summary>
        /// An A.I.'s Firewall.
        /// </summary>
        public int Firewall
        {
            get
            {
                double dblAverage = Convert.ToDouble(_attCHA.TotalValue, GlobalOptions.Instance.CultureInfo) + Convert.ToDouble(_attWIL.TotalValue, GlobalOptions.Instance.CultureInfo);
                dblAverage = Math.Ceiling(dblAverage / 2);
                return Convert.ToInt32(dblAverage);
            }
        }

        /// <summary>
        /// An A.I.'s Signal.
        /// </summary>
        public int Signal
        {
            get
            {
                return _intSignal;
            }
            set
            {
                _intSignal = value;
            }
        }

        /// <summary>
        /// An A.I.'s Response.
        /// </summary>
        public int Response
        {
            get
            {
                return _intResponse;
            }
            set
            {
                _intResponse = value;
            }
        }

        /// <summary>
        /// Maximum Skill Rating.
        /// </summary>
        public int MaxSkillRating
        {
            get
            {
                return _intMaxSkillRating;
            }
            set
            {
                _intMaxSkillRating = value;
            }
        }
        #endregion

        #region Special Attribute Tests
        /// <summary>
        /// Composure (WIL + CHA).
        /// </summary>
        public int Composure
        {
            get
            {
                return _attWIL.TotalValue + _attCHA.TotalValue + _objImprovementManager.ValueOf(Improvement.ImprovementType.Composure);
            }
        }

        /// <summary>
        /// Judge Intentions (INT + CHA).
        /// </summary>
        public int JudgeIntentions
        {
            get
            {
                return _attINT.TotalValue + _attCHA.TotalValue + _objImprovementManager.ValueOf(Improvement.ImprovementType.JudgeIntentions);
            }
        }

        /// <summary>
        /// Lifting and Carrying (STR + BOD).
        /// </summary>
        public int LiftAndCarry
        {
            get
            {
                return _attSTR.TotalValue + _attBOD.TotalValue + _objImprovementManager.ValueOf(Improvement.ImprovementType.LiftAndCarry);
            }
        }

        /// <summary>
        /// Memory (LOG + WIL).
        /// </summary>
        public int Memory
        {
            get
            {
                return _attLOG.TotalValue + _attWIL.TotalValue + _objImprovementManager.ValueOf(Improvement.ImprovementType.Memory);
            }
        }
        #endregion

        #region Reputation
        /// <summary>
        /// Amount of Street Cred the character has earned through standard means.
        /// </summary>
        public int CalculatedStreetCred
        {
            get
            {
                // Street Cred = Career Karma / 10, rounded down
                int intReturn = CareerKarma/10;

                // Deduct burnt Street Cred.
                intReturn -= _intBurntStreetCred;

                return intReturn;
            }
        }

        /// <summary>
        /// Character's total amount of Street Cred (earned + GM awarded).
        /// </summary>
        public int TotalStreetCred
        {
            get
            {
                return Math.Max(CalculatedStreetCred + StreetCred, 0);
            }
        }

        /// <summary>
        /// Street Cred Tooltip.
        /// </summary>
        public string StreetCredTooltip
        {
            get
            {
                string strReturn = "";

                strReturn += "(" + LanguageManager.Instance.GetString("String_CareerKarma") + " (" + CareerKarma.ToString() + ")";
                if (BurntStreetCred != 0)
                    strReturn += " - " + LanguageManager.Instance.GetString("String_BurntStreetCred") + " (" + BurntStreetCred.ToString() + ")";
                strReturn += ") ÷ 10";

                return strReturn;
            }
        }

        /// <summary>
        /// Amount of Notoriety the character has earned through standard means.
        /// </summary>
        public int CalculatedNotoriety
        {
            get
            {
                // Notoriety is simply the total value of Notoriety Improvements + the number of Enemies they have.
                int intReturn = _objImprovementManager.ValueOf(Improvement.ImprovementType.Notoriety);

                foreach (Contact objContact in _lstContacts)
                {
                    if (objContact.EntityType == ContactType.Enemy)
                        intReturn += 1;
                }

                return intReturn;
            }
        }

        /// <summary>
        /// Character's total amount of Notoriety (earned + GM awarded - burnt Street Cred).
        /// </summary>
        public int TotalNotoriety
        {
            get
            {
                return CalculatedNotoriety + Notoriety - (BurntStreetCred / 2);
            }
        }

        /// <summary>
        /// Tooltip to use for Notoriety total.
        /// </summary>
        public string NotorietyTooltip
        {
            get
            {
                string strReturn = "";
                int intEnemies = 0;

                foreach (Improvement objImprovement in _lstImprovements)
                {
                    if (objImprovement.ImproveType == Improvement.ImprovementType.Notoriety)
                        strReturn += " + " + GetObjectName(objImprovement) + " (" + objImprovement.Value.ToString() + ")";
                }

                foreach (Contact objContact in _lstContacts)
                {
                    if (objContact.EntityType == ContactType.Enemy)
                        intEnemies += 1;
                }

                if (intEnemies > 0)
                    strReturn += " + " + LanguageManager.Instance.GetString("Label_SummaryEnemies") + " (" + intEnemies.ToString() + ")";

                if (BurntStreetCred > 0)
                    strReturn += " - " + LanguageManager.Instance.GetString("String_BurntStreetCred") + " (" + (BurntStreetCred / 2).ToString() + ")";

                strReturn = strReturn.Trim();
                if (strReturn.StartsWith("+") || strReturn.StartsWith("-"))
                    strReturn = strReturn.Substring(2, strReturn.Length - 2);

                return strReturn;
            }
        }

        /// <summary>
        /// Amount of Public Awareness the character has earned through standard means.
        /// </summary>
        public int CalculatedPublicAwareness
        {
            get
            {
                // Public Awareness is calculated as (Street Cred + Notoriety) / 3, rounded down.
                double dblAwareness = Convert.ToDouble(TotalStreetCred, GlobalOptions.Instance.CultureInfo) + Convert.ToDouble(TotalNotoriety, GlobalOptions.Instance.CultureInfo);
                dblAwareness = Math.Floor(dblAwareness / 3);

                int intReturn = Convert.ToInt32(dblAwareness);

                if (intReturn < 0)
                    intReturn = 0;

                return intReturn;
            }
        }

        /// <summary>
        /// Character's total amount of Public Awareness (earned + GM awarded).
        /// </summary>
        public int TotalPublicAwareness
        {
            get
            {
                return Math.Max(CalculatedPublicAwareness + PublicAwareness, 0);
            }
        }

        /// <summary>
        /// Public Awareness Tooltip.
        /// </summary>
        public string PublicAwarenessTooltip
        {
            get
            {
                string strReturn = "";

                strReturn += "(" + LanguageManager.Instance.GetString("String_StreetCred") + " (" + TotalStreetCred.ToString() + ") + " + LanguageManager.Instance.GetString("String_Notoriety") + " (" + TotalNotoriety.ToString() + ")) ÷ 3";

                return strReturn;
            }
        }
        #endregion

        #region List Properties
        /// <summary>
        /// Improvements.
        /// </summary>
        public List<Improvement> Improvements
        {
            get
            {
                return _lstImprovements;
            }
        }

        /// <summary>
        /// Skills (Active and Knowledge).
        /// </summary>
        public List<Skill> Skills
        {
            get
            {
                // If the List is not yet populated, go populate it.
                if (_lstSkills.Count == 0)
                    BuildSkillList();
                return _lstSkills;
            }
        }

        /// <summary>
        /// Skill Groups.
        /// </summary>
        public List<SkillGroup> SkillGroups
        {
            get
            {
                // If the List is not yet populated, go populate it.
                if (_lstSkillGroups.Count == 0)
                    BuildSkillGroupList();
                return _lstSkillGroups;
            }
        }

        /// <summary>
        /// Contacts and Enemies.
        /// </summary>
        public List<Contact> Contacts
        {
            get
            {
                return _lstContacts;
            }
        }

        /// <summary>
        /// Spirits and Sprites.
        /// </summary>
        public List<Spirit> Spirits
        {
            get
            {
                return _lstSpirits;
            }
        }

        /// <summary>
        /// Magician Spells.
        /// </summary>
        public List<Spell> Spells
        {
            get
            {
                return _lstSpells;
            }
        }

        /// <summary>
        /// Foci.
        /// </summary>
        public List<Focus> Foci
        {
            get
            {
                return _lstFoci;
            }
        }

        /// <summary>
        /// Stacked Foci.
        /// </summary>
        public List<StackedFocus> StackedFoci
        {
            get
            {
                return _lstStackedFoci;
            }
        }

        /// <summary>
        /// Adept Powers.
        /// </summary>
        public List<Power> Powers
        {
            get
            {
                return _lstPowers;
            }
        }

        /// <summary>
        /// Technomancer Complex Forms.
        /// </summary>
        public List<ComplexForm> ComplexForms
        {
            get
            {
                return _lstComplexForms;
            }
        }

        /// <summary>
        /// Martial Arts.
        /// </summary>
        public List<MartialArt> MartialArts
        {
            get
            {
                return _lstMartialArts;
            }
        }

        /// <summary>
        /// Martial Arts Maneuvers.
        /// </summary>
        public List<MartialArtManeuver> MartialArtManeuvers
        {
            get
            {
                return _lstMartialArtManeuvers;
            }
        }

        /// <summary>
        /// Limit Modifiers.
        /// </summary>
        public List<LimitModifier> LimitModifiers
        {
            get
            {
                return _lstLimitModifiers;
            }
        }

        /// <summary>
        /// Armor.
        /// </summary>
        public List<Armor> Armor
        {
            get
            {
                return _lstArmor;
            }
        }

        /// <summary>
        /// Cyberware and Bioware.
        /// </summary>
        public List<Cyberware> Cyberware
        {
            get
            {
                return _lstCyberware;
            }
        }

        /// <summary>
        /// Weapons.
        /// </summary>
        public List<Weapon> Weapons
        {
            get
            {
                return _lstWeapons;
            }
        }

        /// <summary>
        /// Lifestyles.
        /// </summary>
        public List<Lifestyle> Lifestyles
        {
            get
            {
                return _lstLifestyles;
            }
        }

        /// <summary>
        /// Gear.
        /// </summary>
        public List<Gear> Gear
        {
            get
            {
                return _lstGear;
            }
        }

        /// <summary>
        /// Vehicles.
        /// </summary>
        public List<Vehicle> Vehicles
        {
            get
            {
                return _lstVehicles;
            }
        }

        /// <summary>
        /// Metamagics and Echoes.
        /// </summary>
        public List<Metamagic> Metamagics
        {
            get
            {
                return _lstMetamagics;
            }
        }

        /// <summary>
        /// Enhancements.
        /// </summary>
        public List<Enhancement> Enhancements
        {
            get
            {
                return _lstEnhancements;
            }
        }

        /// <summary>
        /// Arts.
        /// </summary>
        public List<Art> Arts
        {
            get
            {
                return _lstArts;
            }
        }

        /// <summary>
        /// Critter Powers.
        /// </summary>
        public List<CritterPower> CritterPowers
        {
            get
            {
                return _lstCritterPowers;
            }
        }

        /// <summary>
        /// Initiation and Submersion Grades.
        /// </summary>
        public List<InitiationGrade> InitiationGrades
        {
            get
            {
                return _lstInitiationGrades;
            }
        }

        /// <summary>
        /// Expenses (Karma and Nuyen).
        /// </summary>
        public List<ExpenseLogEntry> ExpenseEntries
        {
            get
            {
                return _lstExpenseLog;
            }
        }

        /// <summary>
        /// Qualities (Positive and Negative).
        /// </summary>
        public List<Quality> Qualities
        {
            get
            {
                return _lstQualities;
            }
        }
        /// <summary>
        /// Qualities (Positive and Negative).
        /// </summary>
        public List<LifestyleQuality> LifestyleQualities
        {
            get
            {
                return _lstLifestyleQualities;
            }
        }

        /// <summary>
        /// Life modules
        /// </summary>
        //public List<LifeModule> LifeModules
        //{
        //    get { return _lstLifeModules; }
        //}

        /// <summary>
        /// Locations.
        /// </summary>
        public List<string> Locations
        {
            get
            {
                return _lstLocations;
            }
        }

        /// <summary>
        /// Armor Bundles.
        /// </summary>
        public List<string> ArmorBundles
        {
            get
            {
                return _lstArmorBundles;
            }
        }

        /// <summary>
        /// Weapon Locations.
        /// </summary>
        public List<string> WeaponLocations
        {
            get
            {
                return _lstWeaponLocations;
            }
        }

        /// <summary>
        /// Improvement Groups.
        /// </summary>
        public List<string> ImprovementGroups
        {
            get
            {
                return _lstImprovementGroups;
            }
        }

        /// <summary>
        /// Calendar.
        /// </summary>
        public List<CalendarWeek> Calendar
        {
            get
            {
                return _lstCalendar;
            }
        }
        #endregion

        #region Armor Properties
        /// <summary>
        /// The Character's highest Armor Rating.
        /// </summary>
        public int ArmorRating
        {
            get
            {
                int intHighest = 0;
                int intArmor = 0;

                // Run through the list of Armor currently worn and retrieve the highest total Armor rating.
                foreach (Armor objArmor in _lstArmor)
                {
                    // Don't look at items that start with "+" since we'll consider those next.
                    if (!objArmor.ArmorValue.StartsWith("+"))
                    {
                        if (objArmor.TotalArmor > intHighest && objArmor.Equipped)
                            intHighest = objArmor.TotalArmor;
                    }
                }

                intArmor = intHighest;

                // Run through the list of Armor currently worn again and look at non-Clothing items that start with "+" since they stack with the highest Armor.
                int intStacking = 0;
                foreach (Armor objArmor in _lstArmor)
                {
                    if (objArmor.ArmorValue.StartsWith("+") && objArmor.Category != "Clothing" && objArmor.Equipped)
                        intStacking += objArmor.TotalArmor;
                }

                // Run through the list of Armor currently worn again and look at Clothing items that start with "+" since they stack with eachother.
                int intClothing = 0;
                foreach (Armor objArmor in _lstArmor)
                {
                    if (objArmor.ArmorValue.StartsWith("+") && objArmor.Category == "Clothing" && objArmor.Equipped)
                    {
                        intClothing += objArmor.TotalArmor;
                    }
                }

                if (intClothing > intArmor)
                    intArmor = intClothing;

                return intArmor + intStacking;
            }
        }

        /// <summary>
        /// The Character's total Armor Rating.
        /// </summary>
        public int TotalArmorRating
        {
            get
            {
                int intHighest = 0;
                int intArmor = 0;
                string strHighest;

                // Run through the list of Armor currently worn and retrieve the highest total Armor rating.
                // Form-Fitting Armor is not included in this since it stacks with other worn armor, even if it's lower.
                foreach (Armor objArmor in _lstArmor)
                {
                    if (objArmor.TotalArmor > intHighest && objArmor.Equipped && !objArmor.ArmorValue.StartsWith("+"))
                    {
                        intHighest = objArmor.TotalArmor;
                        strHighest = objArmor.Name;
                    }
                }
                intArmor = intHighest;

                // Run through the list of Armor currently worn again and look at non-Clothing items that start with "+" since they stack with the highest Armor.
                int intStacking = 0;
                foreach (Armor objArmor in _lstArmor)
                {
                    if (objArmor.ArmorValue.StartsWith("+") && objArmor.Category != "High-Fashion Armor Clothing" && objArmor.Category != "Clothing" && objArmor.Equipped)
                    {
                        intStacking += objArmor.TotalArmor;
                    }
                }

                // Run through the list of Armor currently worn again and look at High-Fashion Armor Clothing items that start with "+" since they stack with eachother.
                int intFashionClothing = 0;
                int intFashionClothingStack = 0;
                string strFashionClothing = "";
                foreach (Armor objArmor in _lstArmor)
                {
                    
                    if (objArmor.Equipped && objArmor.Category == "High-Fashion Armor Clothing")
                    {
                        //Find the highest fancy suit armour value.
                        if (!objArmor.ArmorValue.StartsWith("+") && objArmor.TotalArmor > intFashionClothing)
                        {
                            foreach (ArmorMod objMod in objArmor.ArmorMods)
                                if (objMod.Name != "Custom Fit (Stack)")
                                {
                                    intFashionClothing = objArmor.TotalArmor;
                                    strFashionClothing = objArmor.Name;
                                }
                        }
                        //Find the fancy suits that stack with other fancy suits.
                        else if (objArmor.ArmorOverrideValue.StartsWith("+"))
                        {
                            foreach (ArmorMod objMod in objArmor.ArmorMods)
                            {
                                if (objMod.Name == "Custom Fit (Stack)" && objMod.Extra == strFashionClothing)
                                {
                                    intFashionClothingStack += Convert.ToInt32(objArmor.TotalArmor);
                                }
                            }
                        }
                    }
                }
                intFashionClothing += intFashionClothingStack;
                // Run through the list of Armor currently worn again and look at Clothing items that start with "+" since they stack with eachother.
                int intClothing = 0;
                foreach (Armor objArmor in _lstArmor)
                {
                    if (objArmor.ArmorValue.StartsWith("+") && objArmor.Equipped && objArmor.Category == "Clothing")
                    {
                        intClothing += objArmor.TotalArmor;
                    }
                }

                int[] intArmorMax = new[] { intClothing, intArmor, intFashionClothing };
                intArmor = intArmorMax.Max();

                // Add any Armor modifiers.
                intArmor += _objImprovementManager.ValueOf(Improvement.ImprovementType.Armor);

                return intArmor + intStacking;
            }
        }

        /// <summary>
        /// Armor Encumbrance modifier from Armor.
        /// </summary>
        public int ArmorEncumbrance
        {
            get
            {
                int intTotalA = 0;
                foreach (Armor objArmor in _lstArmor)
                    if (objArmor.Equipped)
                        // Form-Fitting Armor is treated as half of its value for determining Armor Encumbrance.
                        if (objArmor.ArmorValue.StartsWith("+"))
                            intTotalA += objArmor.TotalArmor;

                // calculate armor encumberance
                if (intTotalA > this._attSTR.TotalValue)
                    return (intTotalA - this._attSTR.TotalValue) / 2 * -1;  // we expect a negative number
                return 0;
            }
        }

        #endregion

        #region Condition Monitors
        /// <summary>
        /// Number of Physical Condition Monitor boxes.
        /// </summary>
        public int PhysicalCM
        {
            get
            {
                double dblBOD = _attBOD.TotalValue;
                int intCMPhysical = (int)Math.Ceiling(dblBOD / 2) + 8;
                // Include Improvements in the Condition Monitor values.
                intCMPhysical += Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.PhysicalCM));
                if (_strMetatype.EndsWith("A.I.") || _strMetatypeCategory == "Technocritters" || _strMetatypeCategory == "Protosapients")
                {
                    // A.I.s add 1/2 their System to Physical CM since they do not have BOD.
                    double dblSystem = System;
                    intCMPhysical += (int)Math.Ceiling(dblSystem / 2);
                }
                return intCMPhysical;
            }
        }

        /// <summary>
        /// Number of Stun Condition Monitor boxes.
        /// </summary>
        public int StunCM
        {
            get
            {
                double dblWIL = _attWIL.TotalValue;
                int intCMStun = (int)Math.Ceiling(dblWIL / 2) + 8;
                // Include Improvements in the Condition Monitor values.
                intCMStun += Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.StunCM));
                if (_strMetatype.EndsWith("A.I.") || _strMetatypeCategory == "Technocritters" || _strMetatypeCategory == "Protosapients")
                {
                    // A.I. do not have a Stun Condition Monitor.
                    intCMStun = 0;
                }
                return intCMStun;
            }
        }

        /// <summary>
        /// Number of Condition Monitor boxes are needed to reach a Condition Monitor Threshold.
        /// </summary>
        public int CMThreshold
        {
            get
            {
                int intCMThreshold = 0;
                intCMThreshold = 3 + Convert.ToInt32(_objImprovementManager.ValueOf(Improvement.ImprovementType.CMThreshold));
                return intCMThreshold;
            }
        }

        /// <summary>
        /// Number of additioal boxes appear before the first Condition Monitor penalty.
        /// </summary>
        public int CMThresholdOffset
        {
            get
            {
                int intCMThresholdOffset = _objImprovementManager.ValueOf(Improvement.ImprovementType.CMThresholdOffset);
                return intCMThresholdOffset;
            }
        }

        /// <summary>
        /// Number of Overflow Condition Monitor boxes.
        /// </summary>
        public int CMOverflow
        {
            get
            {
                // Characters get a number of overflow boxes equal to their BOD (plus any Improvements). One more boxes is added to mark the character as dead.
                double dblBOD = _attBOD.TotalValue;
                int intCMOverflow = Convert.ToInt32(dblBOD) + _objImprovementManager.ValueOf(Improvement.ImprovementType.CMOverflow) + 1;
                if (_strMetatype.EndsWith("A.I.") || _strMetatypeCategory == "Technocritters" || _strMetatypeCategory == "Protosapients")
                {
                    // A.I. do not have an Overflow Condition Monitor.
                    intCMOverflow = 0;
                }
                return intCMOverflow;
            }
        }

        /// <summary>
        /// Total modifiers from Condition Monitor damage.
        /// </summary>
        public int WoundModifiers
        {
            get
            {
                int intModifier = 0;
                foreach (Improvement objImprovement in _lstImprovements)
                {
                    if (objImprovement.ImproveSource == Improvement.ImprovementSource.ConditionMonitor && objImprovement.Enabled)
                        intModifier += objImprovement.Value;
                }

                return intModifier;
            }
        }
        #endregion

        #region Build Properties
        /// <summary>
        /// Method being used to build the character.
        /// </summary>
        public CharacterBuildMethod BuildMethod
        {
            get
            {
                return _objBuildMethod;
            }
            set
            {
                _objBuildMethod = value;
            }
        }

        /// <summary>
        /// Number of Build Points that are used to create the character.
        /// </summary>
        public int BuildPoints
        {
            get
            {
                return _intBuildPoints;
            }
            set
            {
                _intBuildPoints = value;
            }
        }

		/// <summary>
		/// Number of Build Points that are used to create the character.
		/// </summary>
		public int SumtoTen
		{
			get
			{
				return _intSumtoTen;
			}
			set
			{
				_intSumtoTen = value;
			}
		}
		/// <summary>
		/// Amount of Karma that is used to create the character.
		/// </summary>
		public int BuildKarma
        {
            get
            {
                return _intBuildKarma;
            }
            set
            {
                _intBuildKarma = value;
            }
        }

        /// <summary>
        /// Amount of Nuyen the character has.
        /// </summary>
        public int Nuyen
        {
            get
            {
                return _intNuyen;
            }
            set
            {
                _intNuyen = value;
            }
        }

        /// <summary>
        /// Amount of Nuyen the character started with via the priority system.
        /// </summary>
        public int StartingNuyen
        {
            get
            {
                return _intStartingNuyen;
            }
            set
            {
                _intStartingNuyen = value;
            }
        }

        /// <summary>
        /// Number of Build Points put into Nuyen.
        /// </summary>
        public decimal NuyenBP
        {
            get
            {
                return _decNuyenBP;
            }
            set
            {
                _decNuyenBP = value;
            }
        }

        /// <summary>
        /// Number of Bonded Foci discounted by an Adept Way.
        /// </summary>
        public int AdeptWayDiscount
        {
            get
            {
                return _intAdeptWayDiscount;
            }
            set
            {
                _intAdeptWayDiscount = value;
            }
        }

        /// <summary>
        /// Maximum number of Build Points that can be spent on Nuyen.
        /// </summary>
        public decimal NuyenMaximumBP
        {
            get
            {
                decimal decImprovement = Convert.ToDecimal(_objImprovementManager.ValueOf(Improvement.ImprovementType.NuyenMaxBP), GlobalOptions.Instance.CultureInfo);
                if (_objBuildMethod == CharacterBuildMethod.Karma)
                    decImprovement *= 2.0m;

                // If UnrestrictedNueyn is enabled, return the number of BP or Karma the character is being built with, otherwise use the standard value attached to the character.
                if (_objOptions.UnrestrictedNuyen)
                {
                    if (_intBuildKarma > 0)
                        return _intBuildKarma;
                    else
                        return 1000;
                }
                else
                    return Math.Max(_decNuyenMaximumBP, decImprovement);
            }
            set
            {
                _decNuyenMaximumBP = value;
            }
        }

        /// <summary>
        /// The calculated Astral Limit.
        /// </summary>
        public int LimitAstral
        {
            get
            {
                int intLimit = Math.Max(LimitMental, LimitSocial);
                return intLimit;
            }
        }

        /// <summary>
        /// The calculated Physical Limit.
        /// </summary>
        public int LimitPhysical
        {
            get
            {
                int intLimit = Convert.ToInt32(Math.Ceiling(((Convert.ToDecimal(_attSTR.TotalValue) * 2) + Convert.ToDecimal(_attBOD.TotalValue) + Convert.ToDecimal(_attREA.TotalValue)) / 3));
                intLimit += _objImprovementManager.ValueOf(Improvement.ImprovementType.PhysicalLimit);
                return intLimit;
            }
        }

        /// <summary>
        /// The calculated Mental Limit.
        /// </summary>
        public int LimitMental
        {
            get
            {
                int intLimit = Convert.ToInt32(Math.Ceiling(((Convert.ToDecimal(_attLOG.TotalValue) * 2) + Convert.ToDecimal(_attINT.TotalValue) + Convert.ToDecimal(_attWIL.TotalValue)) / 3));
                intLimit += _objImprovementManager.ValueOf(Improvement.ImprovementType.MentalLimit);
                return intLimit;
            }
        }

        /// <summary>
        /// The calculated Social Limit.
        /// </summary>
        public int LimitSocial
        {
            get
            {
                int intLimit = Convert.ToInt32(Math.Ceiling(((Convert.ToDecimal(_attCHA.TotalValue) * 2) + Convert.ToDecimal(_attWIL.TotalValue) + Math.Ceiling(Essence)) / 3));
                intLimit += _objImprovementManager.ValueOf(Improvement.ImprovementType.SocialLimit);
                return intLimit;
            }
        }

        /// <summary>
        /// Number of free Knowledge Skill Points the character has.
        /// </summary>
        public int KnowledgeSkillPoints
        {
            get
            {
                return _intKnowledgeSkillPoints;
            }
            set
            {
                _intKnowledgeSkillPoints = value;
            }
        }

        /// <summary>
        /// Number of free Knowledge Skill Points the character has used.
        /// </summary>
        public int KnowledgeSkillPointsUsed
        {
            get
            {
                return _intKnowledgeSkills;
            }
            set
            {
                _intKnowledgeSkills = value;
            }
        }

        /// <summary>
        /// Number of free Skill Points the character has.
        /// </summary>
        public int SkillPoints
        {
            get
            {
                return _intSkillPoints;
            }
            set
            {
                _intSkillPoints = value;
            }
        }

        /// <summary>
        /// Number of maximum Skill Points the character has.
        /// </summary>
        public int SkillPointsMaximum
        {
            get
            {
                return _intSkillPointsMaximum;
            }
            set
            {
                _intSkillPointsMaximum = value;
            }
        }

        /// <summary>
        /// Number of free Skill Points the character has.
        /// </summary>
        public int SkillGroupPoints
        {
            get
            {
                return _intSkillGroups;
            }
            set
            {
                _intSkillGroups = value;
            }
        }

        /// <summary>
        /// Number of maximum Skill Groups the character has.
        /// </summary>
        public int SkillGroupPointsMaximum
        {
            get
            {
                return _intSkillGroupsMaximum;
            }
            set
            {
                _intSkillGroupsMaximum = value;
            }
        }
        #endregion

        #region Metatype/Metavariant Information
        /// <summary>
        /// Character's Metatype.
        /// </summary>
        public string Metatype
        {
            get
            {
                return _strMetatype;
            }
            set
            {
                _strMetatype = value;
            }
        }

        /// <summary>
        /// Character's Metavariant.
        /// </summary>
        public string Metavariant
        {
            get
            {
                return _strMetavariant;
            }
            set
            {
                _strMetavariant = value;
            }
        }

        /// <summary>
        /// Metatype Category.
        /// </summary>
        public string MetatypeCategory
        {
            get
            {
                return _strMetatypeCategory;
            }
            set
            {
                _strMetatypeCategory = value;
            }
        }

        /// <summary>
        /// The number of Skill points the Critter had before it became a Mutant Critter.
        /// </summary>
        public int MutantCritterBaseSkills
        {
            get
            {
                return _intMutantCritterBaseSkills;
            }
            set
            {
                _intMutantCritterBaseSkills = value;
            }
        }

        /// <summary>
        /// Character's Movement rate.
        /// </summary>
        public string Movement
        {
            get
            {
                string strReturn = "";

                // Don't attempt to do anything if the character's Movement is "Special" (typically for A.I.s).
                if (_strMovement == "Special")
                    return "Special";

                // If there is no Movement data, read it from the Metatype file, apply it to the character and save the updated file.
                if (_strMovement.Trim() == "")
                {
                    XmlDocument objXmlDocument = new XmlDocument();
                    if (_blnIsCritter)
                        objXmlDocument = XmlManager.Instance.Load("critters.xml");
                    else
                        objXmlDocument = XmlManager.Instance.Load("metatypes.xml");
                    try
                    {
                        _strMovement = "0"; // objXmlDocument.SelectSingleNode("/chummer/metatypes/metatype[name = \"" + _strMetatype + "\"]")["movement"].InnerText;

                        try
                        {
                            string strWalk = objXmlDocument.SelectSingleNode("/chummer/metatypes/metatype[name = \"" + _strMetatype + "\"]")["walk"].InnerText;
                            string strRun = objXmlDocument.SelectSingleNode("/chummer/metatypes/metatype[name = \"" + _strMetatype + "\"]")["run"].InnerText;
                            string strSprint = objXmlDocument.SelectSingleNode("/chummer/metatypes/metatype[name = \"" + _strMetatype + "\"]")["sprint"].InnerText;

                            if (_objOptions.CyberlegMovement)
                            {
                                int intLegs = 0;
                                int intAGI = 0;
                                foreach (Cyberware objCyber in _lstCyberware)
                                {
                                    if (objCyber.LimbSlot == "leg")
                                    {
                                        intLegs++;
                                        if (intAGI > 0)
                                            intAGI = Math.Min(intAGI, objCyber.TotalAgility);
                                        else
                                            intAGI = objCyber.TotalAgility;
                                    }
                                }
                                if (intLegs == 2)
                                    _strMovement = String.Format("{0}/{1}", (intAGI * 2), (intAGI * 4));
                                else
                                    _strMovement = String.Format("{0}/{1}", (_attAGI.TotalValue * 2), (_attAGI.TotalValue * 4));
                            }
                            else
                                _strMovement = String.Format("{0}/{1}", (_attAGI.TotalValue * 2), (_attAGI.TotalValue * 4));
                        }
                        catch
                        { }
                    }
                    catch
                    {
                        _strMovement = "0";
                    }
                    Save();
                }

                string[] strMovements = _strMovement.Split(',');
                foreach (string strMovement in strMovements)
                {
                    // Ignore Fly and Swim information since we're only looking for land Movement.
                    if (!strMovement.Trim().StartsWith("Fly") && !strMovement.Trim().StartsWith("Swim"))
                    {
                        string[] strValue = strMovement.Split('/');
                        int intWalking = Convert.ToInt32(strValue[0]);
                        int intRunning = Convert.ToInt32(strValue[1]);
                        double dblPercent = _objImprovementManager.ValueOf(Improvement.ImprovementType.MovementPercent);
                        dblPercent /= 100.0;

                        intWalking += Convert.ToInt32(Math.Floor(Convert.ToDouble(intWalking, GlobalOptions.Instance.CultureInfo) * dblPercent));
                        intRunning += Convert.ToInt32(Math.Floor(Convert.ToDouble(intRunning, GlobalOptions.Instance.CultureInfo) * dblPercent));

                        strReturn += intWalking.ToString() + "/" + intRunning.ToString();
                    }
                }

                if (strReturn == "")
                    return "0";
                else
                    return strReturn;
            }
            set
            {
                _strMovement = value;
            }
        }

        /// <summary>
        /// Character's walking Movement rate.
        /// </summary>
        private int WalkingRate
        {
            get
            {
                if (!Movement.Contains("/"))
                    return 0;
                else
                {
                    string[] strMovement = Movement.Split('/');
                    return Convert.ToInt32(strMovement[0]);
                }
            }
        }

        /// <summary>
        /// Character's running Movement rate.
        /// </summary>
        private int RunningRate
        {
            get
            {
                if (!Movement.Contains("/"))
                    return 0;
                else
                {
                    string[] strMovement = Movement.Split('/');
                    return Convert.ToInt32(strMovement[1]);
                }
            }
        }

        /// <summary>
        /// Character's Swim rate.
        /// </summary>
        public string Swim
        {
            get
            {
                string strReturn = "";

                // Don't attempt to do anything if the character's Movement is "Special" (typically for A.I.s).
                if (_strMovement == "Special")
                    return "0";

                string[] strMovements = _strMovement.Split(',');
                foreach (string strMovement in strMovements)
                {
                    if (strMovement.Trim().StartsWith("Swim"))
                    {
                        double dblPercent = _objImprovementManager.ValueOf(Improvement.ImprovementType.SwimPercent);
                        dblPercent /= 100.0;

                        string[] strValue = strMovement.Replace("Swim", string.Empty).Trim().Split('/');
                        if (strValue.Length == 1)
                        {
                            int intWalking = Convert.ToInt32(strValue[0]);
                            intWalking += Convert.ToInt32(Math.Floor(Convert.ToDouble(intWalking, GlobalOptions.Instance.CultureInfo) * dblPercent));
                            strReturn = intWalking.ToString();
                        }
                        else
                        {
                            int intWalking = Convert.ToInt32(strValue[0]);
                            int intRunning = Convert.ToInt32(strValue[1]);

                            intWalking += Convert.ToInt32(Math.Floor(Convert.ToDouble(intWalking, GlobalOptions.Instance.CultureInfo) * dblPercent));
                            intRunning += Convert.ToInt32(Math.Floor(Convert.ToDouble(intRunning, GlobalOptions.Instance.CultureInfo) * dblPercent));

                            strReturn += intWalking.ToString() + "/" + intRunning.ToString();
                        }
                    }
                }

                if (strReturn == "")
                    return "0";
                else
                    return strReturn;
            }
        }

        /// <summary>
        /// Character's Fly rate.
        /// </summary>
        public string Fly
        {
            get
            {
                string strReturn = "";

                // Don't attempt to do anything if the character's Movement is "Special" (typically for A.I.s).
                if (_strMovement == "Special")
                    return "0";

                string strCharacterMovement = _strMovement;

                // If the character does not have a Fly speed, see if they get one through Improvements.
                if (!_strMovement.Contains("Fly "))
                {
                    if (_objImprovementManager.ValueOf(Improvement.ImprovementType.FlySpeed) > 0)
                        strCharacterMovement += ",Fly " + _objImprovementManager.ValueOf(Improvement.ImprovementType.FlySpeed).ToString();
                    // If the FlySpeed is a negative number, Fly speed is instead calculated as Momvement Rate * the number given.
                    if (_objImprovementManager.ValueOf(Improvement.ImprovementType.FlySpeed) < 0)
                    {
                        int intMultiply = _objImprovementManager.ValueOf(Improvement.ImprovementType.FlySpeed) * -1;
                        strCharacterMovement += ",Fly " + (WalkingRate * intMultiply).ToString() + "/" + (RunningRate * intMultiply).ToString();
                    }
                }

                string[] strMovements = strCharacterMovement.Split(',');
                foreach (string strMovement in strMovements)
                {
                    if (strMovement.Trim().StartsWith("Fly"))
                    {
                        double dblPercent = _objImprovementManager.ValueOf(Improvement.ImprovementType.FlyPercent);
                        dblPercent /= 100.0;

                        string[] strValue = strMovement.Replace("Fly", string.Empty).Trim().Split('/');
                        if (strValue.Length == 1)
                        {
                            int intWalking = Convert.ToInt32(strValue[0]);
                            intWalking += Convert.ToInt32(Math.Floor(Convert.ToDouble(intWalking, GlobalOptions.Instance.CultureInfo) * dblPercent));
                            strReturn = intWalking.ToString();
                        }
                        else
                        {
                            int intWalking = Convert.ToInt32(strValue[0]);
                            int intRunning = Convert.ToInt32(strValue[1]);

                            intWalking += Convert.ToInt32(Math.Floor(Convert.ToDouble(intWalking, GlobalOptions.Instance.CultureInfo) * dblPercent));
                            intRunning += Convert.ToInt32(Math.Floor(Convert.ToDouble(intRunning, GlobalOptions.Instance.CultureInfo) * dblPercent));

                            strReturn += intWalking.ToString() + "/" + intRunning.ToString();
                        }
                    }
                }

                if (strReturn == "")
                    return "0";
                else
                    return strReturn;
            }
        }

        /// <summary>
        /// Full Movement (Movement, Swim, and Fly) for printouts.
        /// </summary>
        private string FullMovement()
        {
            string strReturn = "";
            if (Movement != "0")
                strReturn += Movement + ", ";
            if (Swim != "0")
                strReturn += LanguageManager.Instance.GetString("Label_OtherSwim") + " " + Swim + ", ";
            if (Fly != "0")
                strReturn += LanguageManager.Instance.GetString("Label_OtherFly") + " " + Fly + ", ";

            // Remove the trailing ", ".
            if (strReturn != "")
                strReturn = strReturn.Substring(0, strReturn.Length - 2);

            return strReturn;
        }

        /// <summary>
        /// BP cost of character's Metatype.
        /// </summary>
        public int MetatypeBP
        {
            get
            {
                return _intMetatypeBP;
            }
            set
            {
                _intMetatypeBP = value;
            }
        }

        /// <summary>
        /// Whether or not the character is a non-Free Sprite.
        /// </summary>
        public bool IsSprite
        {
            get
            {
                if (_strMetatypeCategory.EndsWith("Sprites") && !_strMetatypeCategory.StartsWith("Free"))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Whether or not the character is a Free Sprite.
        /// </summary>
        public bool IsFreeSprite
        {
            get
            {
                if (_strMetatypeCategory == "Free Sprite")
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region Special Functions and Enabled Check Properties
        /// <summary>
        /// Whether or not Adept options are enabled.
        /// </summary>
        public bool AdeptEnabled
        {
            get
            {
                return _blnAdeptEnabled;
            }
            set
            {
                bool blnOldValue = _blnAdeptEnabled;
                _blnAdeptEnabled = value;
                try
                {
                    if (blnOldValue != value)
                        AdeptTabEnabledChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Whether or not Magician options are enabled.
        /// </summary>
        public bool MagicianEnabled
        {
            get
            {
                return _blnMagicianEnabled;
            }
            set
            {
                bool blnOldValue = _blnMagicianEnabled;
                _blnMagicianEnabled = value;
                try
                {
                    if (blnOldValue != value)
                        MagicianTabEnabledChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Whether or not Technomancer options are enabled.
        /// </summary>
        public bool TechnomancerEnabled
        {
            get
            {
                return _blnTechnomancerEnabled;
            }
            set
            {
                bool blnOldValue = _blnTechnomancerEnabled;
                _blnTechnomancerEnabled = value;
                try
                {
                    if (blnOldValue != value)
                        TechnomancerTabEnabledChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Whether or not the Initiation tab should be shown (override for BP mode).
        /// </summary>
        public bool InitiationEnabled
        {
            get
            {
                return _blnInitiationEnabled;
            }
            set
            {
                bool blnOldValue = _blnInitiationEnabled;
                _blnInitiationEnabled = value;
                try
                {
                    if (blnOldValue != value)
                        InitiationTabEnabledChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Whether or not Critter options are enabled.
        /// </summary>
        public bool CritterEnabled
        {
            get
            {
                return _blnCritterEnabled;
            }
            set
            {
                bool blnOldValue = _blnCritterEnabled;
                _blnCritterEnabled = value;
                try
                {
                    if (blnOldValue != value)
                        CritterTabEnabledChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Whether or not Black Market is enabled.
        /// </summary>
        public bool BlackMarket
        {
            get
            {
                return _blnBlackMarket;
            }
            set
            {
                bool blnOldValue = _blnBlackMarket;
                _blnBlackMarket = value;
                try
                {
                    if (blnOldValue != value)
                        BlackMarketEnabledChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Whether or not Uneducated is enabled.
        /// </summary>
        public bool Uneducated
        {
            get
            {
                return _blnUneducated;
            }
            set
            {
                bool blnOldValue = _blnUneducated;
                _blnUneducated = value;
                try
                {
                    if (blnOldValue != value)
                        UneducatedChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not Jack of All Trades is enabled.
        /// </summary>
        public bool JackOfAllTrades
        {
            get
            {
                return _blnJackOfAllTrades;
            }
            set
            {
                bool blnOldValue = _blnJackOfAllTrades;
                _blnJackOfAllTrades = value;
                try
                {
                    if (blnOldValue != value) { }
                    JackOfAllTradesChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Whether or not College Education is enabled.
        /// </summary>
        public bool CollegeEducation
        {
            get
            {
                return _blnCollegeEducation;
            }
            set
            {
                bool blnOldValue = _blnCollegeEducation;
                _blnCollegeEducation = value;
                try
                {
                    if (blnOldValue != value) { }
                    CollegeEducationChanged(this);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Whether or not Friends in High Places is enabled.
        /// </summary>
        public bool FriendsInHighPlaces
        {
            get
            {
                return _blnFriendsInHighPlaces;
            }
            set
            {
                bool blnOldValue = _blnFriendsInHighPlaces;
                _blnFriendsInHighPlaces = value;
                try
                {
                    if (blnOldValue != value)
                        FriendsInHighPlacesChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not Uncouth is enabled.
        /// </summary>
        public bool Uncouth
        {
            get
            {
                return _blnUncouth;
            }
            set
            {
                bool blnOldValue = _blnUncouth;
                _blnUncouth = value;
                try
                {
                    if (blnOldValue != value)
                        UncouthChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not School of Hard Knocks is enabled.
        /// </summary>
        public bool SchoolOfHardKnocks
        {
            get
            {
                return _blnSchoolOfHardKnocks;
            }
            set
            {
                bool blnOldValue = _blnSchoolOfHardKnocks;
                _blnSchoolOfHardKnocks = value;
                try
                {
                    if (blnOldValue != value)
                        SchoolOfHardKnocksChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not Infirm is enabled.
        /// </summary>
        public bool Infirm
        {
            get
            {
                return _blnInfirm;
            }
            set
            {
                bool blnOldValue = _blnInfirm;
                _blnInfirm = value;
                try
                {
                    if (blnOldValue != value)
                        InfirmChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not ExCon is enabled.
        /// </summary>
        public bool ExCon
        {
            get
            {
                return _blnExCon;
            }
            set
            {
                bool blnOldValue = _blnExCon;
                _blnExCon = value;
                try
                {
                    if (blnOldValue != value)
                        ExConChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not TrustFund is enabled.
        /// </summary>
        public bool TrustFund
        {
            get
            {
                return _blnTrustFund;
            }
            set
            {
                bool blnOldValue = _blnTrustFund;
                _blnTrustFund = value;
                try
                {
                    if (blnOldValue != value)
                        TrustFundChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not TechSchool is enabled.
        /// </summary>
        public bool TechSchool
        {
            get
            {
                return _blnTechSchool;
            }
            set
            {
                bool blnOldValue = _blnTechSchool;
                _blnTechSchool = value;
                try
                {
                    if (blnOldValue != value)
                        TechSchoolChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not RestrictedGear is enabled.
        /// </summary>
        public bool RestrictedGear
        {
            get
            {
                return _blnRestrictedGear;
            }
            set
            {
                bool blnOldValue = _blnRestrictedGear;
                _blnRestrictedGear = value;
                try
                {
                    if (blnOldValue != value)
                        RestrictedGearChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not Overclocker is enabled.
        /// </summary>
        public bool Overclocker
        {
            get
            {
                return _blnOverclocker;
            }
            set
            {
                bool blnOldValue = _blnOverclocker;
                _blnOverclocker = value;
                try
                {
                    if (blnOldValue != value)
                        OverclockerChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not MadeMan is enabled.
        /// </summary>
        public bool MadeMan
        {
            get
            {
                return _blnMadeMan;
            }
            set
            {
                bool blnOldValue = _blnMadeMan;
                _blnMadeMan = value;
                try
                {
                    if (blnOldValue != value)
                        MadeManChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not Linguist is enabled.
        /// </summary>
        public bool Linguist
        {
            get
            {
                return _blnLinguist;
            }
            set
            {
                bool blnOldValue = _blnLinguist;
                _blnLinguist = value;
                try
                {
                    if (blnOldValue != value)
                        LinguistChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not LightningReflexes is enabled.
        /// </summary>
        public bool LightningReflexes
        {
            get
            {
                return _blnLightningReflexes;
            }
            set
            {
                bool blnOldValue = _blnLightningReflexes;
                _blnLightningReflexes = value;
                try
                {
                    if (blnOldValue != value)
                        LightningReflexesChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not Fame is enabled.
        /// </summary>
        public bool Fame
        {
            get
            {
                return _blnFame;
            }
            set
            {
                bool blnOldValue = _blnFame;
                _blnFame = value;
                try
                {
                    if (blnOldValue != value)
                        FameChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not BornRich is enabled.
        /// </summary>
        public bool BornRich
        {
            get
            {
                return _blnBornRich;
            }
            set
            {
                bool blnOldValue = _blnBornRich;
                _blnBornRich = value;
                try
                {
                    if (blnOldValue != value)
                        BornRichChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Whether or not Erased is enabled.
        /// </summary>
        public bool Erased
        {
            get
            {
                return _blnErased;
            }
            set
            {
                bool blnOldValue = _blnErased;
                _blnErased = value;
                try
                {
                    if (blnOldValue != value)
                        ErasedChanged(this);
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Convert a string to a CharacterBuildMethod.
        /// </summary>
        /// <param name="strValue">String value to convert.</param>
        public CharacterBuildMethod ConvertToCharacterBuildMethod(string strValue)
        {
            switch (strValue)
            {
                case "Karma":
                    return CharacterBuildMethod.Karma;
                case "SumtoTen":
                    return CharacterBuildMethod.SumtoTen;
				case "LifeModule":
					return CharacterBuildMethod.LifeModule;
                default:
                    return CharacterBuildMethod.Priority;
            }
        }

        /// <summary>
        /// Extended Availability Test information for an item based on the character's Negotiate Skill.
        /// </summary>
        /// <param name="intCost">Item's cost.</param>
        /// <param name="strAvail">Item's Availability.</param>
        public string AvailTest(int intCost, string strAvail)
        {
            string strReturn = "";
            string strInterval = "";
            int intAvail = 0;
            int intTest = 0;

            try
            {
                intAvail = Convert.ToInt32(strAvail.Replace(LanguageManager.Instance.GetString("String_AvailRestricted"), string.Empty).Replace(LanguageManager.Instance.GetString("String_AvailForbidden"), string.Empty));
            }
            catch
            {
                intAvail = 0;
            }

            bool blnCalculate = true;

            if (intAvail == 0 || (!strAvail.Contains(LanguageManager.Instance.GetString("String_AvailRestricted")) && !strAvail.Contains(LanguageManager.Instance.GetString("String_AvailForbidden"))))
                blnCalculate = false;

            if (blnCalculate)
            {
                // Determine the interval based on the item's price.
                if (intCost <= 100)
                    strInterval = "12 " + LanguageManager.Instance.GetString("String_Hours");
                else if (intCost > 100 && intCost <= 1000)
                    strInterval = "1 " + LanguageManager.Instance.GetString("String_Day");
                else if (intCost > 1000 && intCost <= 10000)
                    strInterval = "2 " + LanguageManager.Instance.GetString("String_Days");
                else
                    strInterval = "1 " + LanguageManager.Instance.GetString("String_Week");

                // Find the character's Negotiation total.
                foreach (Skill objSkill in _lstSkills)
                {
                    if (objSkill.Name == "Negotiation")
                        intTest = objSkill.TotalRating;
                }

                strReturn = intTest.ToString() + " (" + intAvail.ToString() + ", " + strInterval + ")";
            }
            else
                strReturn = LanguageManager.Instance.GetString("String_None");

            return strReturn;
        }

        /// <summary>
        /// Whether or not Adapsin is enabled.
        /// </summary>
        public bool AdapsinEnabled
        {
            get
            {
                bool blnReturn = false;
                foreach (Improvement objImprovement in _lstImprovements)
                {
                    if (objImprovement.ImproveType == Improvement.ImprovementType.Adapsin && objImprovement.Enabled)
                    {
                        blnReturn = true;
                        break;
                    }
                }

                return blnReturn;
            }
        }

        /// <summary>
        /// Whether or not Burnout's Way is enabled.
        /// </summary>
        public bool BurnoutEnabled
        {
            get
            {
                bool blnReturn = false;
                foreach (Quality objQuality in _lstQualities)
                {
                    if (objQuality.Name == "The Burnout's Way")
                    {
                        blnReturn = true;
                        break;
                    }
                }

                return blnReturn;
            }
        }

        /// <summary>
        /// Whether or not the character has access to Knowsofts and Linguasofts.
        /// </summary>
        public bool SkillsoftAccess
        {
            get
            {
                foreach (Improvement objImprovement in _lstImprovements)
                {
                    if (objImprovement.ImproveType == Improvement.ImprovementType.SkillsoftAccess && objImprovement.Enabled)
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Determine whether or not the character has any Improvements of a given ImprovementType.
        /// </summary>
        /// <param name="objImprovementType">ImprovementType to search for.</param>
        public bool HasImprovement(Improvement.ImprovementType objImprovementType, bool blnRequireEnabled = false)
        {
            foreach (Improvement objImprovement in _lstImprovements)
            {
                if (objImprovement.ImproveType == objImprovementType)
                {
                    if (!blnRequireEnabled || (blnRequireEnabled && objImprovement.Enabled))
                        return true;
                }
            }

            return false;
        }
        #endregion

        #region Application Properties
        /// <summary>
        /// The frmViewer window being used by the character.
        /// </summary>
        public frmViewer PrintWindow
        {
            get
            {
                return _frmPrintView;
            }
            set
            {
                _frmPrintView = value;
            }
        }
        #endregion

        #region Old Quality Conversion Code
        /// <summary>
        /// Convert Qualities that are still saved in the old format.
        /// </summary>
        private void ConvertOldQualities(XmlNodeList objXmlQualityList)
        {
            XmlDocument objXmlQualityDocument = XmlManager.Instance.Load("qualities.xml");
            XmlDocument objXmlMetatypeDocument = XmlManager.Instance.Load("metatypes.xml");

            // Convert the old Qualities.
            foreach (XmlNode objXmlQuality in objXmlQualityList)
            {
                if (objXmlQuality["name"] == null)
                {
                    _lstOldQualities.Add(objXmlQuality.InnerText);

                    string strForceValue = "";

                    XmlNode objXmlQualityNode = objXmlQualityDocument.SelectSingleNode("/chummer/qualities/quality[name = \"" + GetQualityName(objXmlQuality.InnerText) + "\"]");

                    // Re-create the bonuses for the Quality.
                    if (objXmlQualityNode.InnerXml.Contains("<bonus>"))
                    {
                        // Look for the existing Improvement.
                        foreach (Improvement objImprovement in _lstImprovements)
                        {
                            if (objImprovement.ImproveSource == Improvement.ImprovementSource.Quality && objImprovement.SourceName == objXmlQuality.InnerText && objImprovement.Enabled)
                            {
                                strForceValue = objImprovement.ImprovedName;
                                _lstImprovements.Remove(objImprovement);
                                break;
                            }
                        }
                    }

                    // Convert the item to the new Quality class.
                    Quality objQuality = new Quality(this);
                    List<Weapon> objWeapons = new List<Weapon>();
                    List<TreeNode> objWeaponNodes = new List<TreeNode>();
                    TreeNode objNode = new TreeNode();
                    objQuality.Create(objXmlQualityNode, this, QualitySource.Selected, objNode, objWeapons, objWeaponNodes, strForceValue);
                    _lstQualities.Add(objQuality);

                    // Add any created Weapons to the character.
                    foreach (Weapon objWeapon in objWeapons)
                        _lstWeapons.Add(objWeapon);
                }
            }

            // Take care of the Metatype information.
            XmlNode objXmlMetatype = objXmlMetatypeDocument.SelectSingleNode("/chummer/metatypes/metatype[name = \"" + _strMetatype + "\"]");
            if (objXmlMetatype == null)
            {
                objXmlMetatypeDocument = XmlManager.Instance.Load("critters.xml");
                objXmlMetatype = objXmlMetatypeDocument.SelectSingleNode("/chummer/metatypes/metatype[name = \"" + _strMetatype + "\"]");
            }

            // Positive Qualities.
            foreach (XmlNode objXmlMetatypeQuality in objXmlMetatype.SelectNodes("qualities/positive/quality"))
            {
                bool blnFound = false;
                // See if the Quality already exists in the character.
                foreach (Quality objCharacterQuality in _lstQualities)
                {
                    if (objCharacterQuality.Name == objXmlMetatypeQuality.InnerText)
                    {
                        blnFound = true;
                        break;
                    }
                }

                // If the Quality was not found, create it.
                if (!blnFound)
                {
                    string strForceValue = "";
                    TreeNode objNode = new TreeNode();
                    List<Weapon> objWeapons = new List<Weapon>();
                    List<TreeNode> objWeaponNodes = new List<TreeNode>();
                    Quality objQuality = new Quality(this);

                    if (objXmlMetatypeQuality.Attributes["select"] != null)
                        strForceValue = objXmlMetatypeQuality.Attributes["select"].InnerText;

                    XmlNode objXmlQuality = objXmlQualityDocument.SelectSingleNode("/chummer/qualities/quality[name = \"" + objXmlMetatypeQuality.InnerText + "\"]");
                    objQuality.Create(objXmlQuality, this, QualitySource.Metatype, objNode, objWeapons, objWeaponNodes, strForceValue);
                    _lstQualities.Add(objQuality);

                    // Add any created Weapons to the character.
                    foreach (Weapon objWeapon in objWeapons)
                        _lstWeapons.Add(objWeapon);
                }
            }

            // Negative Qualities.
            foreach (XmlNode objXmlMetatypeQuality in objXmlMetatype.SelectNodes("qualities/negative/quality"))
            {
                bool blnFound = false;
                // See if the Quality already exists in the character.
                foreach (Quality objCharacterQuality in _lstQualities)
                {
                    if (objCharacterQuality.Name == objXmlMetatypeQuality.InnerText)
                    {
                        blnFound = true;
                        break;
                    }
                }

                // If the Quality was not found, create it.
                if (!blnFound)
                {
                    string strForceValue = "";
                    TreeNode objNode = new TreeNode();
                    List<Weapon> objWeapons = new List<Weapon>();
                    List<TreeNode> objWeaponNodes = new List<TreeNode>();
                    Quality objQuality = new Quality(this);

                    if (objXmlMetatypeQuality.Attributes["select"] != null)
                        strForceValue = objXmlMetatypeQuality.Attributes["select"].InnerText;

                    XmlNode objXmlQuality = objXmlQualityDocument.SelectSingleNode("/chummer/qualities/quality[name = \"" + objXmlMetatypeQuality.InnerText + "\"]");
                    objQuality.Create(objXmlQuality, this, QualitySource.Metatype, objNode, objWeapons, objWeaponNodes, strForceValue);
                    _lstQualities.Add(objQuality);

                    // Add any created Weapons to the character.
                    foreach (Weapon objWeapon in objWeapons)
                        _lstWeapons.Add(objWeapon);
                }
            }

            // Do it all over again for Metavariants.
            if (_strMetavariant != "")
            {
                objXmlMetatype = objXmlMetatype.SelectSingleNode("metavariants/metavariant[name = \"" + _strMetavariant + "\"]");

                // Positive Qualities.
                foreach (XmlNode objXmlMetatypeQuality in objXmlMetatype.SelectNodes("qualities/positive/quality"))
                {
                    bool blnFound = false;
                    // See if the Quality already exists in the character.
                    foreach (Quality objCharacterQuality in _lstQualities)
                    {
                        if (objCharacterQuality.Name == objXmlMetatypeQuality.InnerText)
                        {
                            blnFound = true;
                            break;
                        }
                    }

                    // If the Quality was not found, create it.
                    if (!blnFound)
                    {
                        string strForceValue = "";
                        TreeNode objNode = new TreeNode();
                        List<Weapon> objWeapons = new List<Weapon>();
                        List<TreeNode> objWeaponNodes = new List<TreeNode>();
                        Quality objQuality = new Quality(this);

                        if (objXmlMetatypeQuality.Attributes["select"] != null)
                            strForceValue = objXmlMetatypeQuality.Attributes["select"].InnerText;

                        XmlNode objXmlQuality = objXmlQualityDocument.SelectSingleNode("/chummer/qualities/quality[name = \"" + objXmlMetatypeQuality.InnerText + "\"]");
                        objQuality.Create(objXmlQuality, this, QualitySource.Metatype, objNode, objWeapons, objWeaponNodes, strForceValue);
                        _lstQualities.Add(objQuality);

                        // Add any created Weapons to the character.
                        foreach (Weapon objWeapon in objWeapons)
                            _lstWeapons.Add(objWeapon);
                    }
                }

                // Negative Qualities.
                foreach (XmlNode objXmlMetatypeQuality in objXmlMetatype.SelectNodes("qualities/negative/quality"))
                {
                    bool blnFound = false;
                    // See if the Quality already exists in the character.
                    foreach (Quality objCharacterQuality in _lstQualities)
                    {
                        if (objCharacterQuality.Name == objXmlMetatypeQuality.InnerText)
                        {
                            blnFound = true;
                            break;
                        }
                    }

                    // If the Quality was not found, create it.
                    if (!blnFound)
                    {
                        string strForceValue = "";
                        TreeNode objNode = new TreeNode();
                        List<Weapon> objWeapons = new List<Weapon>();
                        List<TreeNode> objWeaponNodes = new List<TreeNode>();
                        Quality objQuality = new Quality(this);

                        if (objXmlMetatypeQuality.Attributes["select"] != null)
                            strForceValue = objXmlMetatypeQuality.Attributes["select"].InnerText;

                        XmlNode objXmlQuality = objXmlQualityDocument.SelectSingleNode("/chummer/qualities/quality[name = \"" + objXmlMetatypeQuality.InnerText + "\"]");
                        objQuality.Create(objXmlQuality, this, QualitySource.Metatype, objNode, objWeapons, objWeaponNodes, strForceValue);
                        _lstQualities.Add(objQuality);

                        // Add any created Weapons to the character.
                        foreach (Weapon objWeapon in objWeapons)
                            _lstWeapons.Add(objWeapon);
                    }
                }
            }
        }

        /// <summary>
        /// Get the name of a Quality by parsing out its BP cost.
        /// </summary>
        /// <param name="strQuality">String to parse.</param>
        private string GetQualityName(string strQuality)
        {
            string strTemp = strQuality;
            int intPos = strTemp.IndexOf('[');

            strTemp = strTemp.Substring(0, intPos - 1);

            return strTemp;
        }
        #endregion

        #region Temporary Properties : Dashboard
        // This region is for properties that are applicable to the Dashboard
        /// <summary>
        /// The Current Initiative roll result including base Initiative
        /// <note>Dashboard</note>
        /// </summary>
        public int InitRoll { get; set; }

        /// <summary>
        /// The Initiative Passes that the player has
        /// <note>Dashboard</note>
        /// </summary>
        public int InitPasses
        {
            get
            {
                if (_initPasses == Int32.MinValue)
                    _initPasses = Convert.ToInt32(this.InitiativePasses);
                return _initPasses;
            }
            set { this._initPasses = value; }
        }
        private int _initPasses = Int32.MinValue;

        /// <summary>
        /// True iff the character is currently delaying an action
        /// <note>Dashboard</note>
        /// </summary>
        public bool Delayed { get; set; }

        /// <summary>
        /// The current name and initiative of the character
        /// </summary>
        public string DisplayInit
        {
            get { return this.Name + " : " + this.InitRoll; }
        }

        /// <summary>
        /// The initial Initiative of the character
        /// <note>Dashboard</note>
        /// </summary>
        public int InitialInit { get; set; }

		//Can't be at improvementmanager due reasons
	    private  Lazy<Stack<String>> _pushtext = new Lazy<Stack<String>>();

		/// <summary>
		/// Push a value that will be used instad of dialog instead in next <selecttext />
		/// </summary>
	    public Stack<String> Pushtext
	    {
		    get
		    {
				return _pushtext.Value;
		    }
	    }

	    #endregion
	}
}