CREATE TABLE [dbo].[TimeEntries] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Date]        DATETIME2 (7)  NOT NULL,
    [Hours]       INT            NOT NULL,
    [Approved]    BIT            NOT NULL,
    [UserId]      NVARCHAR (450) NOT NULL,
    [TimeEntryId] INT            NULL,
    [TimeIn]      TIME (7)       NULL,
    [Time_Out]    TIME (7)       NULL,
    [Day] DATETIME NULL, 
    CONSTRAINT [PK_TimeEntries] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TimeEntries_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TimeEntries_TimeEntries_TimeEntryId] FOREIGN KEY ([TimeEntryId]) REFERENCES [dbo].[TimeEntries] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_TimeEntries_TimeEntryId]
    ON [dbo].[TimeEntries]([TimeEntryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TimeEntries_UserId]
    ON [dbo].[TimeEntries]([UserId] ASC);

