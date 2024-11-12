namespace src.DTO
{
    public class SpecificationsDTO
    {
        public class CreateSpecificationsDto
        {
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

        public class UpdateSpecificationsDto
        {
            public string? ProductName { get; set; }
            public string? ProductType { get; set; }
            public string? CPUSocket { get; set; }
            public string? PCISlot { get; set; }
            public string? RAMType { get; set; }
            public string? GPUInterface { get; set; }
            public string? PSU { get; set; }
            public string? PowerConsumption { get; set; }
        }

        public class ReadSpecificationsDto
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
}
