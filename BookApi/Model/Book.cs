// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace BookApi.Model;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
}
