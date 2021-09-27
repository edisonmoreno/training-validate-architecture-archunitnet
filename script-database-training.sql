--
-- PostgreSQL database dump
--

-- Dumped from database version 12.1
-- Dumped by pg_dump version 12.1

-- Started on 2021-09-27 00:42:38

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 2828 (class 0 OID 0)
-- Dependencies: 3
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 203 (class 1259 OID 57005)
-- Name: Product; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Product" (
    "Id" integer,
    "Status" boolean,
    "Date" timestamp without time zone,
    "Name" character varying,
    "Price" numeric,
    "ImageUrl" character varying,
    "Stock" double precision,
    "StockMin" double precision,
    "StockMax" double precision,
    "UserId" character varying,
    "DateUpdate" timestamp without time zone
);


ALTER TABLE public."Product" OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 56995)
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    "Id" character varying,
    "AccessFailedCount" integer,
    "ConcurrencyStamp" character varying,
    "Email" character varying,
    "EmailConfirmed" boolean,
    "FirstName" character varying,
    "LastName" character varying,
    "LockoutEnabled" boolean,
    "LockoutEnd" timestamp without time zone,
    "NormalizedEmail" character varying,
    "NormalizedUserName" character varying,
    "PasswordHash" character varying,
    "PhoneNumber" character varying,
    "PhoneNumberConfirmed" boolean,
    "SecurityStamp" character varying,
    "TwoFactorEnabled" boolean,
    "UserName" character varying,
    "RegisterDate " timestamp without time zone,
    "Status" character varying
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- TOC entry 2822 (class 0 OID 57005)
-- Dependencies: 203
-- Data for Name: Product; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Product" ("Id", "Status", "Date", "Name", "Price", "ImageUrl", "Stock", "StockMin", "StockMax", "UserId", "DateUpdate") FROM stdin;
1	t	2021-02-02 00:00:00	Producto 01	10000	\N	0	1	10	\N	2021-02-02 00:00:00
2	t	2021-03-01 00:00:00	Producto 02	20000		1	1	20	\N	2021-03-01 00:00:00
\.


--
-- TOC entry 2821 (class 0 OID 56995)
-- Dependencies: 202
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."User" ("Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "RegisterDate ", "Status") FROM stdin;
\.


--
-- TOC entry 2691 (class 1259 OID 57011)
-- Name: user_id_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX user_id_idx ON public."User" USING btree ("Id");


--
-- TOC entry 2692 (class 1259 OID 57001)
-- Name: user_normalizedemail_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX user_normalizedemail_idx ON public."User" USING btree ("NormalizedEmail");


--
-- TOC entry 2693 (class 1259 OID 57003)
-- Name: user_normalizedusername_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX user_normalizedusername_idx ON public."User" USING btree ("NormalizedUserName");


--
-- TOC entry 2694 (class 2606 OID 57012)
-- Name: Product product_userid_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT product_userid_fk FOREIGN KEY ("UserId") REFERENCES public."User"("Id");


-- Completed on 2021-09-27 00:42:38

--
-- PostgreSQL database dump complete
--

