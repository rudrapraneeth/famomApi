using System;
using System.Collections.Generic;
using System.Text;

namespace HomeMade.Core.Enums
{
    public enum UserType
    {
        CUSTOMER = 1,
        CHEF = 2,
        DELIVERY_STAFF = 3,
        INTERNAL_STAFF = 4
    }

    public enum DeliveryType
    {
        PICKUP = 1,
        DELIVERY = 2,
    }

    public enum OrderStatus
    {
        PLACED = 1,
        CANCELED = 2,
        PENDING_PAYMENT = 3,
        COMPLETE = 4,
        NO_SHOW = 5,
        READY_FOR_PICKUP = 6,
        OUT_FOR_DELIVERY = 7
    }

    public enum AvailabilityType
    {
        NOW = 1,
        SCHPREORDER = 2,
        PREORDER = 3,
        NONE = 4
    }

    public enum QuantityType
    {
        Plates = 1,
        KiloGrams = 2,
        Litres = 3,
        Grams = 4,
        MilliLitres = 5,
        Pieces = 6,
        Dozen = 7,
        Packets = 8,
        Boxes = 9
    }
}
