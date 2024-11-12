namespace src.Entity
{
    public class Specifications
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductType { get; set; }

        public string? CPUSocket { get; set; }
        public string? PCISlot { get; set; }
        public string? RAMType { get; set; }
        public string? GPUInterface { get; set; }
        public string? PSU { get; set; }
        public string? PowerConsumption { get; set; }
    }
}
