using Veza.HeatExchanger.Exceptions;
using System;
using System.Collections.Generic;
using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants
{
    /// <summary>
    /// Класс для перевода температуры в давление для хладогента R22
    /// Данные взяты из программы Select 8
    /// </summary>
    sealed internal class RefrigerantR22 : IRefrigerant
    {
        int max = 97;
        int min = -55;
        Dictionary<int, double> tempToPres = new Dictionary<int, double>()
        {
            {-54, 0.52},
            {-53, 0.55},
            {-52, 0.58},
            {-51, 0.61},
            {-50, 0.64},
            {-49, 0.68},
            {-48, 0.71},
            {-47, 0.75},
            {-46, 0.79},
            {-45, 0.83},
            {-44, 0.87},
            {-43, 0.91},
            {-42, 0.95},
            {-41, 1.0},
            {-40, 1.05},
            {-39, 1.10},
            {-38, 1.15},
            {-37, 1.20},
            {-36, 1.26},
            {-35, 1.32},
            {-34, 1.38},
            {-33, 1.44},
            {-32, 1.5},
            {-31, 1.57},
            {-30, 1.63},
            {-29, 1.7},
            {-28, 1.78},
            {-27, 1.85},
            {-26, 1.93},
            {-25, 2.01},
            {-24, 2.09},
            {-23, 2.18},
            {-22, 2.26},
            {-21, 2.35},
            {-20, 2.45},
            {-19, 2.54},
            {-18, 2.64},
            {-17, 2.74},
            {-16, 2.85},
            {-15, 2.96},
            {-14, 3.07},
            {-13, 3.18},
            {-12, 3.3},
            {-11, 3.42},
            {-10, 3.54},
            {-9, 3.67},
            {-8, 3.8},
            {-7, 3.93},
            {-6, 4.07},
            {-5, 4.21},
            {-4, 4.36},
            {-3, 4.51},
            {-2, 4.66},
            {-1, 4.81},
            { 0, 4.97},
            { 1, 5.14},
            { 2, 5.31},
            { 3, 5.48},
            { 4, 5.66},
            { 5, 5.84},
            { 6, 6.02},
            { 7, 6.21},
            { 8, 6.4},
            { 9, 6.6},
            { 10, 6.8},
            { 11, 7.01},
            { 12, 7.22},
            { 13, 7.44},
            { 14, 7.66},
            { 15, 7.89},
            { 16, 8.12},
            { 17, 8.36},
            { 18, 8.6},
            { 19, 8.84},
            { 20, 9.1},
            {21, 9.35},
            {22, 9.62},
            {23, 9.88},
            {24, 10.16},
            {25, 10.44},
            {26, 10.72},
            {27, 11.01},
            {28, 11.31},
            {29, 11.61},
            {30, 11.92},
            {31, 12.23},
            {32, 12.55},
            {33, 12.87},
            {34, 13.21},
            {35, 13.54},
            {36, 13.89},
            {37, 14.24},
            {38, 14.6},
            {39, 14.96},
            {40, 15.33},
            {41, 15.71},
            {42, 16.09},
            {43, 16.48},
            {44, 16.88},
            {45, 17.29},
            {46, 17.7},
            {47, 18.12},
            {48, 18.54},
            {49, 18.98},
            {50, 19.42},
            {51, 19.87},
            {52, 20.32},
            {53, 20.79},
            {54, 21.26},
            {55, 21.74},
            {56, 22.23},
            {57, 22.72},
            {58, 23.23},
            {59, 23.74},
            {60, 24.26},
            {61, 24.79},
            {62, 25.33},
            {63, 25.87},
            {64, 26.43},
            {65, 26.99},
            {66, 27.57},
            {67, 28.15},
            {68, 28.74},
            {69, 29.34},
            {70, 29.95},
            {71, 30.57},
            {72, 31.20},
            {73, 31.84},
            {74, 32.49},
            {75, 33.15},
            {76, 33.82},
            {77, 34.51},
            {78, 35.2},
            {79, 35.9},
            {80, 36.62},
            {81, 37.34},
            {82, 38.08},
            {83, 38.83},
            {84, 39.59},
            {85, 40.36},
            {86, 41.15},
            {87, 41.94},
            {88, 42.75},
            {89, 43.58},
            {90, 44.42},
            {91, 45.27},
            {92, 46.13},
            {93, 47.02},
            {94, 47.91},
            {95, 48.83},
            {96, 49.74},
        };
        Dictionary<double, int> presToTemp = new Dictionary<double, int>()
        {
            {0.52, -54},
            {0.55, -53},
            {0.58, -52},
            {0.61, -51},
            {0.64, -50},
            {0.68, -49},
            {0.71, -48},
            {0.75, -47},
            {0.79, -46},
            {0.83, -45},
            {0.87, -44},
            {0.91, -43},
            {0.95, -42},
            {1.0, -41},
            {1.05, -40},
            {1.10, -39},
            {1.15, -38},
            {1.20, -37},
            {1.26, -36},
            {1.32, -35},
            {1.38, -34},
            {1.44, -33},
            {1.5, -32},
            {1.57, -31},
            {1.63, -30},
            {1.7, -29},
            {1.78, -28},
            {1.85, -27},
            {1.93, -26},
            {2.01, -25},
            {2.09, -24},
            {2.18, -23},
            {2.26, -22},
            {2.35, -21},
            {2.45, -20},
            {2.54, -19},
            {2.64, -18},
            {2.74, -17},
            {2.85, -16},
            {2.96, -15},
            {3.07, -14},
            {3.18, -13},
            {3.3,  -12},
            {3.42, -11},
            {3.54, -10},
            {3.67, -9},
            {3.8,  -8},
            {3.93, -7},
            {4.07, -6},
            {4.21, -5},
            {4.36, -4},
            {4.51, -3},
            {4.66, -2},
            {4.81, -1},
            {4.97, 0},
            {5.14, 1},
            {5.31, 2},
            {5.48, 3},
            {5.66, 4},
            {5.84, 5},
            {6.02, 6},
            {6.21, 7},
            {6.4,  8},
            {6.6,  9},
            {6.8,  10},
            {7.01, 11},
            {7.22, 12},
            {7.44, 13},
            {7.66, 14},
            {7.89, 15},
            {8.12, 16},
            {8.36, 17},
            {8.6,  18},
            {8.84, 19},
            {9.1,  20},
            {9.35, 21},
            {9.62, 22},
            {9.88, 23},
            {10.16, 24},
            {10.44, 25},
            {10.72, 26},
            {11.01, 27},
            {11.31, 28},
            {11.61, 29},
            {11.92, 30},
            {12.23, 31},
            {12.55, 32},
            {12.87, 33},
            {13.21, 34},
            {13.54, 35},
            {13.89, 36},
            {14.24, 37},
            {14.6, 38},
            {14.96, 39},
            {15.33, 40},
            {15.71, 41},
            {16.09, 42},
            {15.48, 43},
            {16.88, 44},
            {17.29, 45},
            {17.7,  46},
            {18.12, 47},
            {18.54, 48},
            {18.98, 49},
            {19.42, 50},
            {19.87, 51},
            {20.32, 52},
            {20.79, 53},
            {21.26, 54},
            {21.74, 55},
            {22.23, 56},
            {22.72, 57},
            {23.23, 58},
            {23.74, 59},
            {24.26, 60},
            {24.79, 61},
            {25.33, 62},
            {25.87, 63},
            {26.43, 64},
            {26.99, 65},
            {27.57, 66},
            {28.15, 67},
            {28.74, 68},
            {29.34, 69},
            {29.95, 70},
            {30.57, 71},
            {31.20, 72},
            {31.84, 73},
            {32.49, 74},
            {33.15, 75},
            {33.82, 76},
            {34.51, 77},
            {35.2, 78},
            {35.9, 79},
            {36.62, 80},
            {37.34, 81},
            {38.08, 82},
            {38.83, 83},
            {39.59, 84},
            {40.36, 85},
            {41.15, 86},
            {41.94, 87},
            {42.75, 88},
            {43.58, 89},
            {44.42, 90},
            {45.27, 91},
            {46.13, 92},
            {47.02, 93},
            {47.91, 94},
            {48.83, 95},
            {49.74, 96},
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
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorTempEvap);
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
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorPresEvap);
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
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorTempCond);
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
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorPresCond);
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
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorSubCol);
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
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorSubColTemp);
        }
        #endregion
    }
}
