﻿namespace Library.Models;

public class Loan
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsReturned { get; set; }
}
