DECLARE @Year INT = ISNULL((SELECT TOP 1 [Year] + 1 FROM EmployeeCalendar.Holiday ORDER BY [Year] DESC), YEAR(GETDATE())) -- prevent unnecessary inserts for every DB update
DECLARE
	@EndYear INT = YEAR(GETDATE()) + 20
,	@EpactCalc INT
,	@PaschalDaysCalc INT
,	@NumOfDaysToSunday INT
,	@EasterMonth INT
,	@EasterDay INT
,	@YearString VARCHAR(4)

WHILE @Year <= @EndYear
BEGIN

	SET @YearString = CAST(@Year AS VARCHAR(4))
	SET @EpactCalc = (24 + 19 * (@Year % 19)) % 30
	SET @PaschalDaysCalc = @EpactCalc - (@EpactCalc / 28)
	SET @NumOfDaysToSunday = @PaschalDaysCalc - ((@Year + @Year / 4 + @PaschalDaysCalc - 13) % 7)
	SET @EasterMonth = 3 + (@NumOfDaysToSunday + 40) / 44
	SET @EasterDay = @NumOfDaysToSunday + 28 - (31 * (@EasterMonth / 4))

	DECLARE @EasterDate DATE = CONVERT(DATE, RTRIM(@Year) + RIGHT('0' + RTRIM(@EasterMonth), 2) + RIGHT('0' + RTRIM(@EasterDay), 2))
	DECLARE @EasterMondayDate DATE = DATEADD(DAY, 1, @EasterDate)
	DECLARE @CorpusChristiDate DATE = DATEADD(DAY, 60, @EasterDate)

	INSERT INTO EmployeeCalendar.Holiday
	(
		[Name]
	,	[Date]
	)
	VALUES
	('New Year''s day', CAST (@YearString + '-01-01' AS DATE)),	-- new year's day
	('Epiphany', CAST (@YearString + '-01-06' AS DATE)),	-- epiphany
	('Easter', @EasterDate),								-- easter
	('Easter Monday', @EasterMondayDate),						-- easter monday
	('Corpus Christi', @CorpusChristiDate),						-- corpus christi
	('International worker''s day', CAST (@YearString + '-05-01' AS DATE)),	-- international worker's day
	('Anti-fascist struggle day', CAST (@YearString + '-06-22' AS DATE)),	-- anti-fascist struggle day
	('Statehood day', CAST (@YearString + '-06-25' AS DATE)),	-- statehood day
	('Victory and Homeland Thanksgiving day', CAST (@YearString + '-08-05' AS DATE)),	-- victory and homeland thanksgiving day
	('Assumption of Mary', CAST (@YearString + '-08-15' AS DATE)),	-- assumption of Mary
	('Independence day', CAST (@YearString + '-10-08' AS DATE)),	-- independence day
	('All Saint''s day', CAST (@YearString + '-11-01' AS DATE)),	-- all saint's day
	('Christmas', CAST (@YearString + '-12-25' AS DATE)),	-- christmas
	('St.Stephen''s day', CAST (@YearString + '-12-26' AS DATE))	-- st. stephen's day

	SET @Year = @Year + 1

END