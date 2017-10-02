-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: acidplayers
-- ------------------------------------------------------
-- Server version	5.7.19-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `blindteam1`
--

DROP TABLE IF EXISTS `blindteam1`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `blindteam1` (
  `user_id` varchar(50) DEFAULT NULL,
  `mmr` int(11) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `wins` int(11) DEFAULT NULL,
  `ready` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `blindteam1`
--

LOCK TABLES `blindteam1` WRITE;
/*!40000 ALTER TABLE `blindteam1` DISABLE KEYS */;
/*!40000 ALTER TABLE `blindteam1` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `blindteam2`
--

DROP TABLE IF EXISTS `blindteam2`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `blindteam2` (
  `mmr` int(11) DEFAULT NULL,
  `user_id` varchar(50) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `wins` int(11) DEFAULT NULL,
  `ready` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `blindteam2`
--

LOCK TABLES `blindteam2` WRITE;
/*!40000 ALTER TABLE `blindteam2` DISABLE KEYS */;
/*!40000 ALTER TABLE `blindteam2` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `game_id`
--

DROP TABLE IF EXISTS `game_id`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `game_id` (
  `id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `game_id`
--

LOCK TABLES `game_id` WRITE;
/*!40000 ALTER TABLE `game_id` DISABLE KEYS */;
INSERT INTO `game_id` VALUES (132);
/*!40000 ALTER TABLE `game_id` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `newgame`
--

DROP TABLE IF EXISTS `newgame`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `newgame` (
  `user_id` varchar(50) DEFAULT NULL,
  `playermmr` int(11) DEFAULT NULL,
  `wins` int(11) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  KEY `user_id` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `newgame`
--

LOCK TABLES `newgame` WRITE;
/*!40000 ALTER TABLE `newgame` DISABLE KEYS */;
INSERT INTO `newgame` VALUES ('146794382312472576',80,NULL,'Pegasus');
/*!40000 ALTER TABLE `newgame` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `playertable`
--

DROP TABLE IF EXISTS `playertable`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `playertable` (
  `mmr` int(11) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `wins` int(11) DEFAULT NULL,
  `user_id` varchar(50) DEFAULT NULL,
  KEY `user_id` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `playertable`
--

LOCK TABLES `playertable` WRITE;
/*!40000 ALTER TABLE `playertable` DISABLE KEYS */;
INSERT INTO `playertable` VALUES (80,'Pegasus',0,'146794382312472576'),(100,'PegasusTest',0,'357718281546629141'),(80,'Greed.CKDa',0,'194941584209477632'),(100,'Morgi',0,'160223769682640896'),(100,'Fonk',0,'147531461073829888'),(121,'thebigmoh',0,'212415933879287809'),(69,'slurms',0,'231497478594101248'),(60,'jaydeepappas',0,'206921047109271552'),(100,'nigelk10',0,'317116458356441088'),(100,'Kuma',0,'130101292809977856'),(100,'Sammich',0,'116016397074694145'),(100,'.trees',0,'344287238567231490'),(100,'Hisoka',0,'119328341105836033'),(100,'LUL',0,'353196612098457622'),(100,'Chicagomed',0,'134813586152226818'),(100,'Therabitier',0,'203998446816985089'),(120,'Tetsuya',0,'264676194824224769'),(100,'BeepinBoopin',0,'219196088278581249'),(100,'DwarveSC',0,'126875675633123328'),(100,'war and pease',0,'194359813658902528'),(100,'SemperPie',0,'111202305596002304'),(110,'Snice',0,'109065760231419904'),(100,'primesoul94',0,'225825679420293121'),(110,'PM[A]',0,'140279869861789705'),(100,'BigMike',0,'291380336263168001'),(100,'Celulon',0,'145692251694039041'),(131,'Springy',0,'138050489789972480'),(90,'Protocol 7',0,'146730810312097792'),(100,'RobotWizard',0,'244621130164469760'),(110,'Gregor',0,'148562472989622273'),(100,'MicroBadger',0,'203673029035687936'),(100,'Ccomfo1',0,'161933484540166144'),(130,'Fish out of water',0,'99937546565328896'),(90,'Siren, meowing jackalope',0,'127109306221985792'),(59,'DannyMcSwangsalot',0,'236350616450891776'),(60,'ButtCheeksMcGee',0,'231515700890435584'),(170,'theshaunsheep',0,'214228072516419585'),(110,'TheVenusProject',0,'188879917033848832'),(110,'s0lar',0,'134332641020346368'),(130,'jothekiller898',0,'172434843223719937'),(90,'QSS',0,'89579039139860480'),(90,'tommytomz',0,'226058438566739968'),(90,'Rapand',0,'195359852409651201'),(100,'Vagrance',0,'83339912023117824'),(100,'πraña',0,'179064075337072641'),(100,'shamed2222',0,'219244288670695424'),(90,'Ape',0,'115584036579049472'),(90,'Malakai',0,'210501775600517120'),(140,'Magician',0,'181887385364201472'),(90,'MoshZombie',0,'102593495641911296'),(171,'aetgy90',0,'276529345084784651'),(120,'Paramore',0,'177207612499165193'),(100,'ya boy',0,'219985756901343233'),(100,'NssN',0,'188452423109574656'),(90,'cello_mello',0,'164103963262582784'),(110,'Mushu (Jeff)',0,'133772863495733248'),(100,'Kodama',0,'135565599639076864'),(100,'penguinsmasher24',0,'318315050169270272'),(100,'GAG-ME',0,'195371393918042112'),(80,'simmons',0,'195371184911679489'),(100,'SEXYcolumboJR',0,'252243985324703746'),(100,'KiDDo',0,'159462595915546624'),(100,'airborneman',0,'134024488894464000'),(110,'XssXTricky',0,'101770210075213824'),(100,'garda',0,'122203538926338049'),(100,'2D',0,'140220916264075265'),(100,'RJDan',0,'139912065211957249'),(100,'NokoZ',0,'177222144311164928'),(100,'Maestro',0,'230523024250634242'),(96,'twamonkey',0,'228615717010800641'),(79,'GAZELLE5333',0,'168218599457161216'),(100,'madior',0,'256567382636494849'),(100,'ikuzo',0,'181504700594520065'),(100,'A Harmless Twig',0,'97076073631608832'),(90,'Scream',0,'211634538525360131'),(100,'EGB',0,'334168003409346563'),(90,'Chicobo!',0,'93536400799973376'),(100,'Snarf|Snarf',0,'77755541761298432'),(100,'Zyhm',0,'134728666486800384'),(100,'Harlemz',0,'180839252404338688'),(100,'Mekar',0,'138057942015541249'),(90,'Life_57',0,'259497097336848384'),(100,'faranrowhani',0,'255802765643743234'),(103,'ansonhunt',0,'253351569599365121'),(97,'Aegon',0,'327898465927430147'),(100,'JQuach07',0,'269188975854354432'),(100,'Trex',0,'215278648092000256'),(110,'Thrashdota',0,'118066524685729794'),(100,'looking for lol gf',0,'285187591740063746'),(100,'8waves8',0,'229721254951780353'),(100,'maloik',0,'55292893362925568'),(100,'Rain',0,'179945962490429451'),(90,'chilled_wind',0,'142451924917157889'),(90,'Satan',0,'155037026817671168'),(110,'Lone Dog',0,'334351382691971114'),(100,'jk',0,'248322483684507648'),(90,'killerkarate',0,'224026551862165504'),(90,'CupOfJoe',0,'224543674808139776'),(110,'Nvr',0,'236317042704121856'),(100,'Nashetania',0,'163122412219531264'),(100,'Tabasco',0,'357576875813371905'),(100,'NTTHRASH',0,'132556103002095616'),(100,'Galaxy',0,'150498668817022976'),(100,'Vizkor',0,'204768396514951177'),(100,'bazhuka23',0,'211352509413851146'),(100,'Aces',0,'364181777071341578'),(100,'Wyvern',0,'177198034227036160'),(100,'Swaginitus',0,'104753119543492608'),(100,'super future',0,'104975712900939776'),(110,'Okina-Yume',0,'245653129914220547'),(100,'Cam',0,'152896054763716608'),(100,'originalmct',0,'207313586547064832');
/*!40000 ALTER TABLE `playertable` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `result`
--

DROP TABLE IF EXISTS `result`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `result` (
  `game_id` int(11) DEFAULT NULL,
  `team1score` int(11) DEFAULT NULL,
  `team2score` int(11) DEFAULT NULL,
  `captain_ready` int(11) DEFAULT NULL,
  KEY `game_id` (`game_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `result`
--

LOCK TABLES `result` WRITE;
/*!40000 ALTER TABLE `result` DISABLE KEYS */;
INSERT INTO `result` VALUES (120,0,7,0),(121,0,0,0),(122,0,0,0),(123,0,7,2),(124,0,0,0),(125,7,0,0),(126,0,0,0),(127,0,0,0),(128,0,0,0),(130,1,10,0),(131,0,0,0);
/*!40000 ALTER TABLE `result` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `team1`
--

DROP TABLE IF EXISTS `team1`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team1` (
  `user_id` varchar(50) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `mmr` int(11) DEFAULT NULL,
  `wins` int(11) DEFAULT NULL,
  `ready` int(10) DEFAULT NULL,
  KEY `user_id` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `team1`
--

LOCK TABLES `team1` WRITE;
/*!40000 ALTER TABLE `team1` DISABLE KEYS */;
/*!40000 ALTER TABLE `team1` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `team2`
--

DROP TABLE IF EXISTS `team2`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team2` (
  `user_id` varchar(50) DEFAULT NULL,
  `username` varchar(50) DEFAULT NULL,
  `mmr` int(11) DEFAULT NULL,
  `wins` int(11) DEFAULT NULL,
  `ready` int(10) DEFAULT NULL,
  KEY `user_id` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `team2`
--

LOCK TABLES `team2` WRITE;
/*!40000 ALTER TABLE `team2` DISABLE KEYS */;
/*!40000 ALTER TABLE `team2` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-10-02 11:29:11
