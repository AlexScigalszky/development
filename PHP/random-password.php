<?php
class PasswordHelper
{
    /**
     * Generate a new random alphanumeric password. Only with number, and letters
     */
    public function newRandom()
    {
        $len = 8;
        $pool = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
        $str = '';
        for ($i = 0; $i < $len; $i++) {
            $str .= substr($pool, mt_rand(0, strlen($pool) - 1), 1);
        }
        return $str;
    }
}
