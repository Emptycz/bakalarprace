-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- Počítač: localhost
-- Vytvořeno: Čtv 25. úno 2021, 20:02
-- Verze serveru: 8.0.18
-- Verze PHP: 7.3.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Databáze: `bachalor`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `csv`
--

CREATE TABLE `csv` (
  `ID` int(11) NOT NULL,
  `RecordID` int(11) NOT NULL,
  `RowNumber` int(11) NOT NULL,
  `Date` varchar(12) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Time` varchar(12) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Result` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Mod` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Best_Match` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Best_Match_MN` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Best_Match_2` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Best_Match_2_MN` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Best_Match_3` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Best_Match_3_MN` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Time_1` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Time_2` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Time_all` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `LOD Sigma` varchar(10) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key1` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value1` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key2` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value2` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key3` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value3` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key4` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value4` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key5` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value5` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key6` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value6` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key7` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value7` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key8` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value8` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key9` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value9` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key10` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value10` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key11` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value11` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key12` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value12` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key13` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value13` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key14` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value14` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key15` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value15` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Key16` varchar(40) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Value16` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Mg` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Mg_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Al` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Al_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Si` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Si_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `P_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `S` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `S_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Cl` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Cl_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `K` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `K_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Ca` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Ca_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Ti` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Ti_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Cr` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Cr_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Mn` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Mn_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Fe` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Fe_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Co` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Co_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Ni` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Ni_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Cu` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Cu_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Zn` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Zn_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `As` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `As_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Se` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Se_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Rb` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Rb_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Sr` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Sr_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Y` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Y_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Zr` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Zr_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Mo` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Mo_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Ag` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Ag_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Cd` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Cd_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `In` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `In_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Sn` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Sn_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Sb` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Sb_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Ba` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Ba_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `W` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `W_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Au` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Au_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Hg` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Hg_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Tl` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Tl_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Pb` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Pb_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Bi` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Bi_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Th` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Th_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `U` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `U_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `LE` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `LE_Advanced` varchar(20) COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabulky `record`
--

CREATE TABLE `record` (
  `ID` int(11) NOT NULL,
  `Name` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `AuthorID` int(11) NOT NULL,
  `Uploaded` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Location` varchar(200) COLLATE utf8mb4_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struktura tabulky `user`
--

CREATE TABLE `user` (
  `ID` int(11) NOT NULL,
  `Firstname` varchar(200) COLLATE utf8mb4_general_ci NOT NULL,
  `Surname` varchar(200) COLLATE utf8mb4_general_ci NOT NULL,
  `Email` varchar(250) COLLATE utf8mb4_general_ci NOT NULL,
  `Password` varchar(255) COLLATE utf8mb4_general_ci NOT NULL,
  `Level` set('User','Admin') COLLATE utf8mb4_general_ci NOT NULL DEFAULT 'User'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Klíče pro exportované tabulky
--

--
-- Klíče pro tabulku `csv`
--
ALTER TABLE `csv`
  ADD PRIMARY KEY (`ID`);

--
-- Klíče pro tabulku `record`
--
ALTER TABLE `record`
  ADD PRIMARY KEY (`ID`);

--
-- Klíče pro tabulku `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT pro tabulky
--

--
-- AUTO_INCREMENT pro tabulku `csv`
--
ALTER TABLE `csv`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pro tabulku `record`
--
ALTER TABLE `record`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pro tabulku `user`
--
ALTER TABLE `user`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
