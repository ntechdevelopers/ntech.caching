CREATE Table Products (
	DataValue int
);

DECLARE @RowCount INT
DECLARE @RowString VARCHAR(10)
SET @RowCount = 0
WHILE @RowCount < 300
BEGIN
    SET @RowString = CAST(@RowCount AS VARCHAR(10))

    INSERT INTO Products
        (DataValue)
    VALUES
        (REPLICATE('0', 10 - DATALENGTH(@RowString)) + @RowString)

    SET @RowCount = @RowCount + 1
END