if not exists (select * from sys.schemas where name='Response')
begin
exec ('create schema Response');
end
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='277CABillProv') drop table Response.[277CABillProv]
go
CREATE TABLE [Response].[277CABillProv](
	[HeaderId] [bigint] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[BillProvName] [varchar](200) NULL,
	[BillProvIdQual] [varchar](50) NULL,
	[BillProvId] [varchar](50) NULL,
	[ClaimId] [varchar](50) NULL,
	[BillProvSecondIdQual1] [varchar](50) NULL,
	[BillProvSecondId1] [varchar](50) NULL,
	[BillProvSecondIdQual2] [varchar](50) NULL,
	[BillProvSecondId2] [varchar](50) NULL,
	[BillProvSecondIdQual3] [varchar](50) NULL,
	[BillProvSecondId3] [varchar](50) NULL,
	[BillProvAcceptedQuantity] [varchar](50) NULL,
	[BillProvRejectedQuantity] [varchar](50) NULL,
	[BillProvAcceptedAmount] [varchar](50) NULL,
	[BillProvRejectedAmount] [varchar](50) NULL,
 CONSTRAINT [PK_277CABillProv] PRIMARY KEY CLUSTERED 
(
	[HeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='277CAFile') drop table Response.[277CAFile]
go
CREATE TABLE [Response].[277CAFile](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[SenderId] [varchar](50) NOT NULL,
	[ReceiverId] [varchar](50) NOT NULL,
	[SenderName] [varchar](200) NOT NULL,
	[ReceiverName] [varchar](200) NOT NULL,
	[TransactionDate] [varchar](50) NOT NULL,
	[TransactionTime] [varchar](50) NOT NULL,
	[ICN] [varchar](50) NOT NULL,
	[BatchId] [varchar](50) NOT NULL,
	[TotalAcceptedQuantity] [varchar](50) NULL,
	[TotalRejectedQuantity] [varchar](50) NULL,
	[TotalAcceptedAmount] [varchar](50) NULL,
	[TotalRejectedAmount] [varchar](50) NULL,
	[CreateUser] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_277CAFile] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='277CALine') drop table Response.[277CALine]
go
CREATE TABLE [Response].[277CALine](
	[LineId] [bigint] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[ClaimId] [varchar](50) NOT NULL,
	[ProcedureQual] [varchar](50) NULL,
	[ProcedureCode] [varchar](50) NULL,
	[Modifier1] [varchar](50) NULL,
	[Modifier2] [varchar](50) NULL,
	[Modifier3] [varchar](50) NULL,
	[Modifier4] [varchar](50) NULL,
	[LineChargeAmount] [varchar](50) NULL,
	[RevenueCode] [varchar](50) NULL,
	[UnitCount] [varchar](50) NULL,
	[LineItemControlNumber] [varchar](50) NULL,
	[PrescriptionNumber] [varchar](50) NULL,
	[ServiceDateFrom] [varchar](50) NULL,
	[ServiceDateTo] [varchar](50) NULL,
 CONSTRAINT [PK_277CALine] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='277CAPatient') drop table Response.[277CAPatient]
go
CREATE TABLE [Response].[277CAPatient](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[ClaimId] [varchar](50) NULL,
	[BillProvId] [varchar](50) NULL,
	[PatientLastName] [varchar](200) NULL,
	[PatientFirstName] [varchar](200) NULL,
	[PatientMI] [varchar](50) NULL,
	[PatientIdQual] [varchar](50) NULL,
	[PatientId] [varchar](50) NULL,
	[PayerClaimControlNumber] [varchar](50) NULL,
	[ClearingHouseTraceNumber] [varchar](50) NULL,
	[BillType] [varchar](50) NULL,
	[ServiceDateFrom] [varchar](50) NULL,
	[ServiceDateTo] [varchar](50) NULL,
 CONSTRAINT [PK_277CAPatient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='277CAStc') drop table Response.[277CAStc]
go
CREATE TABLE [Response].[277CAStc](
	[StcId] [bigint] IDENTITY(1,1) NOT NULL,
	[StcType] [varchar](50) NOT NULL,
	[FileId] [int] NOT NULL,
	[ClaimId] [varchar](50) NULL,
	[BillProvId] [varchar](50) NULL,
	[PatientId] [varchar](50) NULL,
	[ClaimStatusCategory1] [varchar](50) NULL,
	[ClaimStatusCode1] [varchar](50) NULL,
	[EntityIDentifier1] [varchar](50) NULL,
	[StatusInfoEffDate] [varchar](50) NULL,
	[ActionCode] [varchar](50) NULL,
	[ChargeAmount] [varchar](50) NULL,
	[ClaimStatusCategory2] [varchar](50) NULL,
	[ClaimStatusCode2] [varchar](50) NULL,
	[EntityIDentifier2] [varchar](50) NULL,
	[ClaimStatusCategory3] [varchar](50) NULL,
	[ClaimStatusCode3] [varchar](50) NULL,
	[EntityIDentifier3] [varchar](50) NULL,
 CONSTRAINT [PK_277CAStc] PRIMARY KEY CLUSTERED 
(
	[StcId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='999Element') drop table Response.[999Element]
go
CREATE TABLE [Response].[999Element](
	[ElementId] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[TransactionControlNumber] [varchar](50) NULL,
	[PositionInTransaction] [varchar](50) NULL,
	[PositionInSegment] [varchar](50) NULL,
	[ElementReferenceNumber] [varchar](50) NULL,
	[ElementErrorCode] [varchar](50) NULL,
	[ElementBadDataCopy] [varchar](255) NULL,
	[ElementSegmentCode] [varchar](50) NULL,
	[ElementSegmentPositionInTransaction] [varchar](50) NULL,
	[ElementLoopCode] [varchar](50) NULL,
	[ElementPositionInSegment] [varchar](50) NULL,
	[ElementReferenceInSegment] [varchar](50) NULL,
 CONSTRAINT [PK_999Element] PRIMARY KEY CLUSTERED 
(
	[ElementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='999Error') drop table Response.[999Error]
go
CREATE TABLE [Response].[999Error](
	[ErrorId] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[TransactionControlNumber] [varchar](50) NULL,
	[SegmentCode] [varchar](50) NULL,
	[PositionInTransaction] [varchar](50) NULL,
	[LoopCode] [varchar](50) NULL,
	[ErrorCode] [varchar](50) NULL,
	[BusinessUnitName] [varchar](50) NULL,
	[BusinessUnitCode] [varchar](50) NULL,
	[CtxSegmentCode] [varchar](50) NULL,
	[CtxPositionInTransaction] [varchar](50) NULL,
	[CtxLoopCode] [varchar](50) NULL,
	[CtxPositionInSegment] [varchar](50) NULL,
	[CtxReferenceInSegment] [varchar](50) NULL,
 CONSTRAINT [PK_999Error] PRIMARY KEY CLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='999File') drop table Response.[999File]
go
CREATE TABLE [Response].[999File](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[ReceiverId] [varchar](50) NULL,
	[SenderId] [varchar](50) NULL,
	[ICN] [varchar](50) NULL,
	[TransactionDate] [varchar](50) NULL,
	[TransactionTime] [varchar](50) NULL,
	[TransactionsIncluded] [varchar](50) NULL,
	[TransactionsReceived] [varchar](50) NULL,
	[TransactionsAccepted] [varchar](50) NULL,
	[FileAckCode] [varchar](50) NULL,
	[ProductionFlag] [varchar](50) NULL,
	[FileError1] [varchar](50) NULL,
	[FileError2] [varchar](50) NULL,
	[FileError3] [varchar](50) NULL,
	[FileError4] [varchar](50) NULL,
	[FileError5] [varchar](50) NULL,
	[CreateUser] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_999File] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='999Transaction') drop table Response.[999Transaction]
go
CREATE TABLE [Response].[999Transaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[TransactionControlNumber] [varchar](50) NULL,
	[TransactionAckCode] [varchar](50) NULL,
	[TransactionError1] [varchar](50) NULL,
	[TransactionError2] [varchar](50) NULL,
	[TransactionError3] [varchar](50) NULL,
	[TransactionError4] [varchar](50) NULL,
	[TransactionError5] [varchar](50) NULL,
 CONSTRAINT [PK_999Transaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='DHCSEncounter') drop table Response.[DHCSEncounter]
go
CREATE TABLE [Response].[DHCSEncounter](
	[EncounterId] [bigint] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[TransactionNumber] [varchar](50) NULL,
	[EncounterStatus] [varchar](50) NULL,
	[EncounterReferenceNumber] [varchar](50) NULL,
	[DHCSEncounterId] [varchar](50) NULL,
 CONSTRAINT [PK_DHCSEncounter] PRIMARY KEY CLUSTERED 
(
	[EncounterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='DHCSEncounterResponse') drop table Response.[DHCSEncounterResponse]
go
CREATE TABLE [Response].[DHCSEncounterResponse](
	[EncounterResponseId] [bigint] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[TransactionNumber] [varchar](50) NULL,
	[EncounterReferenceNumber] [varchar](50) NULL,
	[Severity] [varchar](50) NULL,
	[IssueId] [varchar](50) NULL,
	[IsSNIP] [varchar](50) NULL,
	[IssueDescription] [varchar](2000) NULL,
 CONSTRAINT [PK_DHCSEncounterResponse] PRIMARY KEY CLUSTERED 
(
	[EncounterResponseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='DHCSFile') drop table Response.[DHCSFile]
go
CREATE TABLE [Response].[DHCSFile](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](200) NULL,
	[EncounterFileName] [varchar](200) NULL,
	[SubmitterName] [varchar](200) NULL,
	[SubmissionDate] [varchar](200) NULL,
	[ValidationStatus] [varchar](50) NULL,
	[CreateUser] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DHCSFile] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='DHCSTransaction') drop table Response.[DHCSTransaction]
go
CREATE TABLE [Response].[DHCSTransaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[TransactionStatus] [varchar](50) NULL,
	[TransactionNumber] [varchar](50) NULL,
	[ISAControlNumber] [varchar](50) NULL,
	[GroupControlNumber] [varchar](50) NULL,
	[OriginatorTransactionId] [varchar](50) NULL,
 CONSTRAINT [PK_DHCSTransaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='MAO2Detail') drop table Response.[MAO2Detail]
go
CREATE TABLE [Response].[MAO2Detail](
	[DetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[ClaimId] [varchar](50) NULL,
	[InternalControlNumber] [varchar](50) NULL,
	[LineNumber] [varchar](50) NULL,
	[EncounterStatus] [varchar](50) NULL,
	[ErrorCode] [varchar](50) NULL,
	[ErrorDescription] [varchar](200) NULL,
 CONSTRAINT [PK_MAO2Detail] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
if exists (select * from sys.tables where schema_name(schema_id)='Response' and name='MAO2File') drop table Response.[MAO2File]
go
CREATE TABLE [Response].[MAO2File](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[SenderId] [varchar](50) NULL,
	[ICN] [varchar](50) NULL,
	[TransactionDate] [varchar](50) NULL,
	[RecordType] [varchar](50) NULL,
	[ProductionFlag] [varchar](50) NULL,
	[TotalErrors] [varchar](50) NULL,
	[TotalLinesAccepted] [varchar](50) NULL,
	[TotalLinesRejected] [varchar](50) NULL,
	[TotalLinesSubmitted] [varchar](50) NULL,
	[TotalEncountersAccepted] [varchar](50) NULL,
	[TotalEncountersRejected] [varchar](50) NULL,
	[TotalEncountersSubmitted] [varchar](50) NULL,
	[CreateUser] [varchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MAO2File] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
