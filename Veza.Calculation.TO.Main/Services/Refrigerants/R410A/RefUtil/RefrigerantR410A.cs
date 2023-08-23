using System;
using System.Collections.Generic;
using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants.R410A
{
    /// <summary>
    /// Класс для перевода температуры в давление для хладогента R22
    /// Данные взяты из программы Select 8
    /// </summary>
    sealed internal class RefrigerantR410A_RefUt : IRefrigerant
    {
        int max = 69;
        int min = -66;
        Dictionary<int, double> tempToPres = new Dictionary<int, double>()
        {
            {-65, 0.514},
            {-64, 0.544},
            {-63, 0.576},
            {-62, 0.608},
            {-61, 0.643},
            {-60, 0.679},
            {-59, 0.716},
            {-58, 0.755},
            {-57, 0.796},
            {-56, 0.839},
            {-55, 0.883},
            {-54, 0.929},
            {-53, 0.977},
            {-52, 1.027},
            {-51, 1.079},
            {-50, 1.134},
            {-49, 1.190},
            {-48, 1.248},
            {-47, 1.309},
            {-46, 1.372},
            {-45, 1.438},
            {-44, 1.506},
            {-43, 1.576},
            {-42, 1.649},
            {-41, 1.725},
            {-40, 1.803},
            {-39, 1.884},
            {-38, 1.968},
            {-37, 2.054},
            {-36, 2.144},
            {-35, 2.237},
            {-34, 2.333},
            {-33, 2.432},
            {-32, 2.534},
            {-31, 2.640},
            {-30, 2.749},
            {-29, 2.861},
            {-28, 2.977},
            {-27, 3.097},
            {-26, 3.220},
            {-25, 3.347},
            {-24, 3.478},
            {-23, 3.613},
            {-22, 3.751},
            {-21, 3.894},
            {-20, 4.041},
            {-19, 4.193},
            {-18, 4.348},
            {-17, 4.508},
            {-16, 4.673},
            {-15, 4.842},
            {-14, 5.016},
            {-13, 5.194},
            {-12, 5.378},
            {-11, 5.566},
            {-10, 5.759},
            {-9, 5.957},
            {-8, 6.161},
            {-7, 6.370},
            {-6, 6.584},
            {-5, 6.803},
            {-4, 7.029},
            {-3, 7.259},
            {-2, 7.496},
            {-1, 7.738},
            { 0, 7.986},
            { 1, 8.241},
            { 2, 8.501},
            { 3, 8.768},
            { 4, 9.041},
            { 5, 9.320},
            { 6, 9.606},
            { 7, 9.898},
            { 8, 10.198},
            { 9, 10.504},
            { 10, 10.817},
            { 11, 11.137},
            { 12, 11.464},
            { 13, 11.798},
            { 14, 12.14},
            { 15, 12.489},
            { 16, 12.846},
            { 17, 13.210},
            { 18, 13.582},
            { 19, 13.962},
            { 20, 14.350},
            { 21, 14.747},
            { 22, 15.151},
            { 23, 15.564},
            { 24, 15.985},
            { 25, 16.415},
            { 26, 16.854},
            { 27, 17.301},
            { 28, 17.758},
            { 29, 18.223},
            { 30, 18.698},
            { 31, 19.182},
            { 32, 19.676},
            { 33, 20.179},
            { 34, 20.692},
            { 35, 21.214},
            { 36, 21.747},
            { 37, 22.29},
            { 38, 22.843},
            { 39, 23.406},
            { 40, 23.981},
            { 41, 24.565},
            { 42, 25.161},
            { 43, 25.767},
            { 44, 26.385},
            { 45, 27.014},
            { 46, 27.654},
            { 47, 28.306},
            { 48, 28.97},
            { 49, 29.645},
            { 50, 30.333},
            { 51, 31.033},
            { 52, 31.745},
            { 53, 32.469},
            { 54, 33.207},
            { 55, 33.957},
            { 56, 34.720},
            { 57, 35.497},
            { 58, 36.287},
            { 59, 37.091},
            { 60, 37.908},
            { 61, 38.739},
            { 62, 39.585},
            { 63, 40.445},
            { 64, 41.320},
            { 65, 42.209},
            { 66, 43.114},
            { 67, 44.035},
            { 68, 44.971},
        };
        Dictionary<double, int> presToTemp = new Dictionary<double, int>()
        {
            {0.514, -65},
            {0.54, 4-64},
            {0.576, -63},
            {0.608, -62},
            {0.643, -61},
            {0.679, -60},
            {0.716, -59},
            {0.755, -58},
            {0.796, -57},
            {0.839, -56},
            {0.883, -55},
            {0.929, -54},
            {0.977, -53},
            {1.027, -52},
            {1.079, -51},
            {1.134, -50},
            {1.190, -49},
            {1.248, -48},
            {1.309, -47},
            {1.372, -46},
            {1.438, -45},
            {1.506, -44},
            {1.576, -43},
            {1.649, -42},
            {1.725, -41},
            {1.803, -40},
            {1.884, -39},
            {1.968, -38},
            {2.054, -37},
            {2.144, -36},
            {2.237, -35},
            {2.333, -34},
            {2.432, -33},
            {2.534, -32},
            {2.640, -31},
            {2.749, -30},
            {2.861, -29},
            {2.977, -28},
            {3.097, -27},
            {3.220, -26},
            {3.347, -25},
            {3.478, -24},
            {3.613, -23},
            {3.751, -22},
            {3.894, -21},
            {4.041, -20},
            {4.193, -19},
            {4.348, -18},
            {4.508, -17},
            {4.673, -16},
            {4.842, -15},
            {5.016, -14},
            {5.194, -13},
            {5.378, -12},
            {5.566, -11},
            {5.759, -10},
            {5.957, -9},
            {6.161, -8},
            {6.370, -7},
            {6.584, -6},
            {6.803, -5},
            {7.029, -4},
            {7.259, -3},
            {7.496, -2},
            {7.738, -1},
            {7.986, 0},
            {8.241, 1},
            {8.501, 2},
            {8.768, 3},
            {9.041, 4},
            {9.320, 5},
            {9.606, 6},
            {9.898, 7},
            {10.198, 8},
            {10.504, 9},
            {10.817, 10},
            {11.137, 11},
            {11.464, 12 },
            {11.798, 13 },
            {12.14 , 14},
            {12.489, 15},
            {12.846, 16 },
            {13.210, 17 },
            {13.582, 18 },
            {13.962, 19 },
            {14.350, 20 },
            {14.747, 21 },
            {15.151, 22 },
            {15.564, 23 },
            {15.985, 24 },
            {16.415, 25 },
            {16.854, 26 },
            {17.301, 27 },
            {17.758, 28 },
            {18.223, 29 },
            {18.698, 30 },
            {19.182, 31 },
            {19.676, 32 },
            {20.179, 33 },
            {20.692, 34 },
            {21.214, 35 },
            {21.747, 36 },
            {22.29 , 37 },
            {22.843, 38 },
            {23.406, 39 },
            {23.981, 40 },
            {24.565, 41 },
            {25.161, 42 },
            {25.767, 43 },
            {26.385, 44 },
            {27.014, 45 },
            {27.654, 46 },
            {28.306, 47 },
            {28.97 , 48},
            {29.645, 49 },
            {30.333, 50 },
            {31.033, 51 },
            {31.745, 52 },
            {32.469, 53 },
            {33.207, 54 },
            {33.957, 55 },
            {34.720, 56 },
            {35.497, 57 },
            {36.287, 58 },
            {37.091, 59 },
            {37.908, 60 },
            {38.739, 61 },
            {39.585, 62 },
            {40.445, 63 },
            {41.320, 64 },
            {42.209, 65 },
            {43.114, 66 },
            {44.035, 67 },
            {44.971, 68 },
        };

        #region Температура кипения
        public double ToPressure(double temperature)
        {
            int temp = (int)temperature;
            if (temp < max)
            {
                if (temp > min)
                {
                    double result = tempToPres[temp];
                    if ((temperature - temp) == 0)
                        return result;
                    if (temperature > 0)
                    {
                        double resultAbove = tempToPres[temp + 1];
                        return result + (resultAbove - result) * (temperature - temp);
                    }
                    if (temperature < 0)
                    {
                        double resultLess = tempToPres[temp - 1];
                        return result + (result - resultLess) * (temperature - temp);
                    }
                }
            }
            return 0; // TODO: вызвать исключение
        }

        public double ToTemperature(double pressure)
        {
            int value;
            if (presToTemp.TryGetValue(pressure, out value))
            {
                return value;
            }
            else
            {
                //var keys = presToTemp.Keys;
                double delta = 1000;
                double keyRes = 0;
                foreach (var key in presToTemp.Keys)
                {
                    double res = Math.Abs(pressure - key);
                    if (res < delta)
                    {
                        delta = res;
                        keyRes = key;
                    }
                }
                if (keyRes != 0)
                {
                    if (presToTemp.TryGetValue(keyRes, out value))
                    {
                        if (pressure > keyRes)
                        {
                            double keyAbove = tempToPres[value + 1];
                            return (pressure - keyRes) / (keyAbove - keyRes) + value;
                        }
                        else
                        {
                            if (value > (min + 1))
                            {
                                double keyLess = tempToPres[value - 1];
                                return value - (keyRes - pressure) / (keyRes - keyLess);
                            }
                        }
                    }
                }
            }
            return 0;
        }
        #endregion

        #region Температура конденсации
        public double ToCondPressure(double temperature)
        {
            int temp = (int)temperature;
            if (temp < max)
            {
                if (temp > min)
                {
                    double result = tempToPres[temp];
                    if ((temperature - temp) == 0)
                        return result;

                    double resultAbove = tempToPres[temp + 1];
                    return result + (resultAbove - result) * (temperature - temp);
                }
            }
            return 0;
        }

        public double ToCondTemperature(double pressure)
        {
            int value;
            if (presToTemp.TryGetValue(pressure, out value))
            {
                return value;
            }
            else
            {
                //var keys = presToTemp.Keys;
                double delta = 1000;
                double keyRes = 0;
                foreach (var key in presToTemp.Keys)
                {
                    double res = Math.Abs(pressure - key);
                    if (res < delta)
                    {
                        delta = res;
                        keyRes = key;
                    }
                }
                if (keyRes != 0)
                {
                    if (presToTemp.TryGetValue(keyRes, out value))
                    {
                        if (pressure > keyRes)
                        {
                            double keyAbove = tempToPres[value + 1];
                            return (pressure - keyRes) / (keyAbove - keyRes) + value;
                        }
                        else
                        {
                            if (value > (min + 1))
                            {
                                if (value > (min + 1))
                                {
                                    double keyLess = tempToPres[value - 1];
                                    return value - (keyRes - pressure) / (keyRes - keyLess);
                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }
        #endregion

        #region Переохлаждение
        public double ToSubCol(double tempCond, double temperature)
        {
            int temp = (int)tempCond;
            if (temp < max)
            {
                if (temp > min)
                {
                    double result = temp;
                    if ((tempCond - temp) == 0)
                    {
                        return result - temperature;
                    }
                    double resultAbove = temp + 1;
                    return result + (resultAbove - result) * (tempCond - temp) - temperature;
                }
            }
            return 0;
        }

        public double ToSubColTemperature(double tempCond, double tempSubCol)
        {
            int temp = (int)tempCond;
            if (temp < max)
            {
                if (temp > min)
                {
                    double result = temp;
                    if ((tempCond - temp) == 0)
                    {
                        return result - tempSubCol;
                    }
                    double resultAbove = temp + 1;
                    return result + (resultAbove - result) * (tempCond - temp) - tempSubCol;
                }
            }
            return 0;
        }
        #endregion
    }
}