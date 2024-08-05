﻿namespace HMS.Client.Models.Input
{
    public class InputModel
    {
        //Especifico ao HMS.Client MicroService
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public long CPF { get; set; }
        public long? RG { get; set; }
        public DateTime DateBirth { get; set; }
        public short YearsOld { get; set; }
    }
}
