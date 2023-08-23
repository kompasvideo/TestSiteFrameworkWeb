using Veza.HeatExchanger.Exceptions;
using System;
using System.Collections.Generic;
using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerants.R410A
{
    /// <summary>
    /// Класс для перевода температуры в давление для хладогента R410A
    /// Данные взяты из программы Select 8
    /// </summary>
    sealed internal class RefrigerantR410A : IRefrigerant
    {
        int max = 69;
        int min = -66;
        Dictionary<int, double> tempToPres = new Dictionary<int, double>()
        {
            {-65, 0.49},
            {-64, 0.52},
            {-63, 0.55},
            {-62, 0.58},
            {-61, 0.61},
            {-60, 0.65},
            {-59, 0.68},
            {-58, 0.72},
            {-57, 0.76},
            {-56, 0.8},
            {-55, 0.85},
            {-54, 0.89},
            {-53, 0.94},
            {-52, 0.99},
            {-51, 1.04},
            {-50, 1.1},
            {-49, 1.15},
            {-48, 1.21},
            {-47, 1.27},
            {-46, 1.33},
            {-45, 1.4},
            {-44, 1.46},
            {-43, 1.53},
            {-42, 1.61},
            {-41, 1.68},
            {-40, 1.76},
            {-39, 1.84},
            {-38, 1.92},
            {-37, 2.01},
            {-36, 2.1},
            {-35, 2.19},
            {-34, 2.29},
            {-33, 2.39},
            {-32, 2.49},
            {-31, 2.59},
            {-30, 2.7},
            {-29, 2.81},
            {-28, 2.93},
            {-27, 3.05},
            {-26, 3.17},
            {-25, 3.3},
            {-24, 3.43},
            {-23, 3.57},
            {-22, 3.71},
            {-21, 3.85},
            {-20, 4.0},
            {-19, 4.15},
            {-18, 4.31},
            {-17, 4.47},
            {-16, 4.63},
            {-15, 4.8},
            {-14, 4.98},
            {-13, 5.16},
            {-12, 5.34},
            {-11, 5.53},
            {-10, 5.72},
            {-9, 5.92},
            {-8, 6.13},
            {-7, 6.34},
            {-6, 6.55},
            {-5, 6.78},
            {-4, 7.0},
            {-3, 7.23},
            {-2, 7.47},
            {-1, 7.72},
            { 0, 7.97},
            { 1, 8.22},
            { 2, 8.49},
            { 3, 8.75},
            { 4, 9.03},
            { 5, 9.31},
            { 6, 9.6},
            { 7, 9.89},
            { 8, 10.2},
            { 9, 10.5},
            { 10, 10.82},
            { 11, 11.14},
            { 12, 11.47},
            { 13, 11.81},
            { 14, 12.15},
            { 15, 12.51},
            { 16, 12.87},
            { 17, 13.23},
            { 18, 13.61},
            { 19, 13.99},
            { 20, 14.38},
            { 21, 14.78},
            { 22, 15.19},
            { 23, 15.61},
            { 24, 16.03},
            { 25, 16.47},
            { 26, 16.91},
            { 27, 17.36},
            { 28, 17.82},
            { 29, 18.29},
            { 30, 18.77},
            { 31, 19.26},
            { 32, 19.76},
            { 33, 20.26},
            { 34, 20.78},
            { 35, 21.31},
            { 36, 21.85},
            { 37, 22.4},
            { 38, 22.96},
            { 39, 23.53},
            { 40, 24.11},
            { 41, 24.7},
            { 42, 25.3},
            { 43, 25.91},
            { 44, 26.54},
            { 45, 27.18},
            { 46, 27.82},
            { 47, 28.48},
            { 48, 29.16},
            { 49, 29.84},
            { 50, 30.54},
            { 51, 31.25},
            { 52, 31.97},
            { 53, 32.71},
            { 54, 33.46},
            { 55, 34.22},
            { 56, 35.0},
            { 57, 35.79},
            { 58, 36.59},
            { 59, 37.41},
            { 60, 38.24},
            { 61, 39.09},
            { 62, 39.95},
            { 63, 40.83},
            { 64, 41.72},
            { 65, 42.63},
            { 66, 43.55},
            { 67, 44.5},
            { 68, 45.45},
        };
        Dictionary<double, int> presToTemp = new Dictionary<double, int>()
        {
            {0.49, -65},
            {0.52, 4-64},
            {0.55, -63},
            {0.58, -62},
            {0.61, -61},
            {0.65, -60},
            {0.68, -59},
            {0.72, -58},
            {0.76, -57},
            {0.8, -56},
            {0.85, -55},
            {0.89, -54},
            {0.94, -53},
            {0.99, -52},
            {1.04, -51},
            {1.1, -50},
            {1.15, -49},
            {1.21, -48},
            {1.27, -47},
            {1.33, -46},
            {1.4, -45},
            {1.46, -44},
            {1.53, -43},
            {1.61, -42},
            {1.68, -41},
            {1.76, -40},
            {1.84, -39},
            {1.92, -38},
            {2.01, -37},
            {2.1, -36},
            {2.19, -35},
            {2.29, -34},
            {2.39, -33},
            {2.49, -32},
            {2.59, -31},
            {2.7, -30},
            {2.81, -29},
            {2.93, -28},
            {3.05, -27},
            {3.17, -26},
            {3.3, -25},
            {3.43, -24},
            {3.57, -23},
            {3.71, -22},
            {3.85, -21},
            {4.0, -20},
            {4.15, -19},
            {4.31, -18},
            {4.47, -17},
            {4.63, -16},
            {4.8, -15},
            {4.98, -14},
            {5.16, -13},
            {5.34, -12},
            {5.53, -11},
            {5.72, -10},
            {5.92, -9},
            {6.13, -8},
            {6.34, -7},
            {6.55, -6},
            {6.78, -5},
            {7.0, -4},
            {7.23, -3},
            {7.47, -2},
            {7.72, -1},
            {7.97, 0},
            {8.22, 1},
            {8.49, 2},
            {8.75, 3},
            {9.03, 4},
            {9.31, 5},
            {9.6, 6},
            {9.89, 7},
            {10.2, 8},
            {10.5, 9},
            {10.82, 10},
            {11.14, 11},
            {11.47, 12 },
            {11.81, 13 },
            {12.15, 14},
            {12.51, 15},
            {12.87, 16 },
            {13.23, 17 },
            {13.61, 18 },
            {13.99, 19 },
            {14.38, 20 },
            {14.78, 21 },
            {15.19, 22 },
            {15.61, 23 },
            {16.03, 24 },
            {16.47, 25 },
            {16.91, 26 },
            {17.36, 27 },
            {17.82, 28 },
            {18.29, 29 },
            {18.77, 30 },
            {19.26, 31 },
            {19.76, 32 },
            {20.26, 33 },
            {20.78, 34 },
            {21.31, 35 },
            {21.85, 36 },
            {22.4 , 37 },
            {22.96, 38 },
            {23.53, 39 },
            {24.11, 40 },
            {24.7 , 41 },
            {25.3 , 42 },
            {25.91, 43 },
            {26.54, 44 },
            {27.18, 45 },
            {27.82, 46 },
            {28.48, 47 },
            {28.97, 48},
            {29.16, 49 },
            {30.54, 50 },
            {31.25, 51 },
            {31.97, 52 },
            {32.71, 53 },
            {33.46, 54 },
            {34.22, 55 },
            {35   , 56 },
            {35.79, 57 },
            {36.59, 58 },
            {37.41, 59 },
            {38.24, 60 },
            {39.09, 61 },
            {39.95, 62 },
            {40.83, 63 },
            {41.72, 64 },
            {42.63, 65 },
            {43.55, 66 },
            {44.5 , 67 },
            {45.45, 68 },
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
        int maxSC = 69;
        int minSC = -66;
        Dictionary<int, double> scToTemp = new Dictionary<int, double>()
        {
            {-65, -65.05},
            {-64, -64.04},
            {-63, -63.04},
            {-62, -62.04},
            {-61, -61.04},
            {-60, -60.04},
            {-59, -59.04},
            {-58, -58.04},
            {-57, -57.04},
            {-56, -56.04},
            {-55, -55.04},
            {-54, -54.05},
            {-53, -53.05},
            {-52, -52.05},
            {-51, -51.05},
            {-50, -50.05},
            {-49, -49.05},
            {-48, -48.05},
            {-47, -47.05},
            {-46, -46.05},
            {-45, -45.05},
            {-44, -44.06},
            {-43, -43.06},
            {-42, -42.06},
            {-41, -41.06},
            {-40, -40.06},
            {-39, -39.06},
            {-38, -38.06},
            {-37, -37.06},
            {-36, -36.06},
            {-35, -35.06},
            {-34, -34.07},
            {-33, -33.07},
            {-32, -32.07},
            {-31, -31.07},
            {-30, -30.07},
            {-29, -29.07},
            {-28, -28.07},
            {-27, -27.07},
            {-26, -26.07},
            {-25, -25.07},
            {-24, -24.07},
            {-23, -23.07},
            {-22, -22.08},
            {-21, -21.08},
            {-20, -20.08},
            {-19, -19.08},
            {-18, -18.08},
            {-17, -17.08},
            {-16, -16.08},
            {-15, -15.08},
            {-14, -14.08},
            {-13, -13.08},
            {-12, -12.08},
            {-11, -11.08},
            {-10, -10.08},
            {-9, -9.08},
            {-8, -8.08},
            {-7, -7.09},
            {-6, -6.09},
            {-5, -5.09},
            {-4, -4.09},
            {-3, -3.09},
            {-2, -2.09},
            {-1, -1.09},
            {0, -0.09},
            {1, 0.91},
            {2, 1.91},
            {3, 2.91},
            {4, 3.91},
            {5, 4.91},
            {6, 5.91},
            {7, 6.90},
            {8, 7.90},
            {9, 8.90},
            {10, 9.90},
            {11, 10.90},
            {12, 11.90},
            {13, 12.90},
            {14, 13.90},
            {15, 14.90},
            {16, 15.90},
            {17, 16.90},
            {18, 17.89},
            {19, 18.89},
            {20, 19.89},
            {21, 20.89},
            {22, 21.89},
            {23, 22.89},
            {24, 23.89},
            {25, 24.89},
            {26, 25.89},
            {27, 26.89},
            {28, 27.88},
            {29, 28.88},
            {30, 29.88},
            {31, 30.88},
            {32, 31.88},
            {33, 32.88},
            {34, 33.88},
            {35, 34.88},
            {36, 35.88},
            {37, 36.88},
            {38, 37.88},
            {39, 38.88},
            {40, 39.88},
            {41, 40.88},
            {42, 41.88},
            {43, 42.88},
            {44, 43.88},
            {45, 44.88},
            {46, 45.88},
            {47, 46.88},
            {48, 47.88},
            {49, 48.88},
            {50, 49.89},
            {51, 50.89},
            {52, 51.89},
            {53, 52.89},
            {54, 53.89},
            {55, 54.90},
            {56, 55.90},
            {57, 56.90},
            {58, 57.91},
            {59, 58.91},
            {60, 59.92},
            {61, 60.92},
            {62, 61.93},
            {63, 62.94},
            {64, 63.94},
            {65, 64.95},
            {66, 65.96},
            {67, 66.97},
            {68, 67.55},
        };
        public double ToSubCol(double tempCond, double temperature)
        {
            int temp = (int)tempCond;
            if (temp < maxSC)
            {
                if (temp > minSC)
                {
                    double result = scToTemp[temp];
                    if ((tempCond - temp) == 0)
                    {
                        return result - temperature;
                    }
                    double resultAbove = scToTemp[temp + 1];
                    return result + (resultAbove - result) * (tempCond - temp) - temperature;
                }
            }
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorSubCol);
        }

        public double ToSubColTemperature(double tempCond, double tempSubCol)
        {
            int temp = (int)tempCond;
            if (temp < maxSC)
            {
                if (temp > minSC)
                {
                    double result = scToTemp[temp];
                    if ((tempCond - temp) == 0)
                    {
                        return result - tempSubCol;
                    }
                    double resultAbove = scToTemp[temp + 1];
                    return result + (resultAbove - result) * (tempCond - temp) - tempSubCol;
                }
            }
            throw new TempToPresException(Calculation.TO.Main.Properties.Resources.ErrorSubColTemp);
        }
        #endregion
    }
}