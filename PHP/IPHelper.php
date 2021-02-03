<?php

class IPHelper
{
    /**
     * Get the location data from an IP adresss
     */
    public function getLocationDataIpAddress(string $ipAddress)
    {
        $ipdat = @json_decode(file_get_contents(
            "http://www.geoplugin.net/json.gp?ip=" . $ipAddress
        ));

        return [
            'Country_Name' => $ipdat->geoplugin_countryName,
            'City_Name' => $ipdat->geoplugin_city,
            'Continent_Name' => $ipdat->geoplugin_continentName,
            'Latitude' => $ipdat->geoplugin_latitude,
            'Longitude' => $ipdat->geoplugin_longitude,
            'Currency_Symbol' => $ipdat->geoplugin_currencySymbol,
            'Currency_Code' => $ipdat->geoplugin_currencyCode,
            'Timezone' => $ipdat->geoplugin_timezone,
            'Country_Code' => $ipdat->geoplugin_countryCode
        ];
    }
}
