using Veza.HeatExchanger.Exceptions;
using System;
using System.Collections.Generic;
using Veza.HeatExchanger.Interfaces.Refrigerants;

namespace Veza.HeatExchanger.Services.Refrigerant
{
    /// <summary>
    /// Класс для перевода температуры в давление для хладогента R404A
    /// Данные взяты из программы Select 8
    /// </summary>
    sealed internal class RefrigerantR404A : IRefrigerant
    {
        int max = 73;
        int min = -60;
        Dictionary<int, double> tempToPres = new Dictionary<int, double>()
        {
            {-59, 0.51},
            {-58, 0.54},
            {-57, 0.57},
            {-56, 0.6},
            {-55, 0.64},
            {-54, 0.67},
            {-53, 0.71},
            {-52, 0.74},
            {-51, 0.78},
            {-50, 0.82},
            {-49, 0.87},
            {-48, 0.91},
            {-47, 0.95},
            {-46, 1},
            {-45, 1.05},
            {-44, 1.10},
            {-43, 1.16},
            {-42, 1.21},
            {-41, 1.27},
            {-40, 1.33},
            {-39, 1.39},
            {-38, 1.45},
            {-37, 1.52},
            {-36, 1.59},
            {-35, 1.66},
            {-34, 1.73},
            {-33, 1.8},
            {-32, 1.88},
            {-31, 1.96},
            {-30, 2.04},
            {-29, 2.13},
            {-28, 2.22},
            {-27, 2.31},
            {-26, 2.4},
            {-25, 2.5},
            {-24, 2.6},
            {-23, 2.7},
            {-22, 2.81},
            {-21, 2.92},
            {-20, 3.03},
            {-19, 3.15},
            {-18, 3.26},
            {-17, 3.39},
            {-16, 3.51},
            {-15, 3.64},
            {-14, 3.77},
            {-13, 3.91},
            {-12, 4.05},
            {-11, 4.19},
            {-10, 4.34},
            {-9, 4.49},
            {-8, 4.65},
            {-7, 4.81},
            {-6, 4.97},
            {-5, 5.14},
            {-4, 5.31},
            {-3, 5.49},
            {-2, 5.67},
            {-1, 5.85},
            { 0, 6.04},
            { 1, 6.24},
            { 2, 6.44},
            { 3, 6.64},
            { 4, 6.85},
            { 5, 7.06},
            { 6, 7.28},
            { 7, 7.5},
            { 8, 7.73},
            { 9, 7.96},
            { 10, 8.2},
            { 11, 8.44},
            { 12, 8.69},
            { 13, 8.95},
            { 14, 9.21},
            { 15, 9.47},
            { 16, 9.74},
            { 17, 10.02},
            { 18, 10.3},
            { 19, 10.59},
            { 20, 10.89},
            { 21, 11.19},
            { 22, 11.49},
            { 23, 11.81},
            {24, 12.13},
            {25, 12.45},
            {26, 12.78},
            {27, 13.12},
            {28, 13.47},
            {29, 13.82},
            {30, 14.18},
            {31, 14.55},
            {32, 14.92},
            {33, 15.3},
            {34, 15.69},
            {35, 16.05},
            {36, 16.49},
            {37, 16.9},
            {38, 17.31},
            {39, 17.74},
            {40, 18.17},
            {41, 18.62},
            {42, 19.07},
            {43, 19.52},
            {44, 19.99},
            {45, 20.47},
            {46, 20.95},
            {47, 21.44},
            {48, 21.95},
            {49, 22.46},
            {50, 22.98},
            {51, 23.51},
            {52, 24.05},
            {53, 24.6},
            {54, 25.16},
            {55, 25.73},
            {56, 26.31},
            {57, 26.9},
            {58, 27.5},
            {59, 28.12},
            {60, 27.74},
            {61, 29.37},
            {62, 30.02},
            {63, 30.68},
            {64, 31.35},
            {65, 32.03},
            {66, 32.73},
            {67, 33.44},
            {68, 34.16},
            {69, 34.89},
            {70, 35.64},
            {71, 36.4},
            {72, 37.09},
        };
        Dictionary<double, int> presToTemp = new Dictionary<double, int>()
        {
            {0.51, -59},
            {0.54, -58},
            {0.57, -57},
            {0.6, -56},
            {0.64, -55},
            {0.67, -54},
            {0.71, -53},
            {0.74, -52},
            {0.78, -51},
            {0.82, -50},
            {0.87, -49},
            {0.91, -48},
            {0.95, -47},
            {1, -46},
            {1.05, -45},
            {1.10, -44},
            {1.16, -43},
            {1.21, -42},
            {1.27, -41},
            {1.33, -40},
            {1.39, -39},
            {1.45, -38},
            {1.52, -37},
            {1.59, -36},
            {1.66, -35},
            {1.73, -34},
            {1.8, -33},
            {1.88, -32},
            {1.96, -31},
            {2.04, -30},
            {2.13, -29},
            {2.22, -28},
            {2.31, -27},
            {2.4, -26},
            {2.5, -25},
            {2.6, -24},
            {2.7, -23},
            {2.81, -22},
            {2.92, -21},
            {3.03, -20},
            {3.15, -19},
            {3.26, -18},
            {3.39, -17},
            {3.51, -16},
            {3.64, -15},
            {3.77, -14},
            {3.91, -13},
            {4.05, -12},
            {4.19, -11},
            {4.34, -10},
            {4.49, -9},
            {4.65, -8},
            {4.81, -7},
            {4.97, -6},
            {5.14, -5},
            {5.31, -4},
            {5.49, -3},
            {5.67, -2},
            {5.85, -1},
            { 6.04, 0},
            { 6.24, 1},
            { 6.44, 2},
            { 6.64, 3},
            { 6.85, 4},
            { 7.06, 5},
            { 7.28, 6},
            { 7.5, 7},
            { 7.73, 8},
            { 7.96, 9},
            { 8.2, 10},
            { 8.44, 11},
            { 8.69, 12},
            { 8.95, 13},
            { 9.21, 14},
            { 9.47, 15},
            { 9.74, 16},
            { 10.02, 17},
            { 10.3, 18},
            { 10.59, 19},
            { 10.89, 20},
            {11.19, 21},
            {11.49, 22},
            {11.81, 23},
            {12.13, 24},
            {12.45, 25},
            {12.78, 26},
            {13.12, 27},
            {13.47, 28},
            {13.82, 29},
            {14.18, 30},
            {14.55, 31},
            {14.92, 32},
            {15.3 , 33},
            {15.69, 34},
            {16.05, 35},
            {16.49, 36},
            {16.9 , 37},
            {17.31, 38},
            {17.74, 39},
            {18.17, 40},
            {18.62, 41},
            {19.07, 42},
            {19.52, 43},
            {19.99, 44},
            {20.47, 45},
            {20.95, 46},
            {21.44, 47},
            {21.95, 48},
            {22.46, 49},
            {22.98, 50},
            {23.51, 51},
            {24.05, 52},
            {24.6 , 53},
            {25.16, 54},
            {25.73, 55},
            {26.31, 56},
            {26.9 , 57},
            {27.5 , 58},
            {28.12, 59},
            {27.74, 60},
            {29.37, 61},
            {30.02, 62},
            {30.68, 63},
            {31.35, 64},
            {32.03, 65},
            {32.73, 66},
            {33.44, 67},
            {34.16, 68},
            {34.89, 69},
            {35.64, 70},
            {36.4, 71},
            {37.09, 72},
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
        int maxSC = 73;
        int minSC = -60;
        Dictionary<int, double> scToTemp = new Dictionary<int, double>()
        {
            {-59, -59.87},
            {-58, -58.87},
            {-57, -57.86},
            {-56, -56.85},
            {-55, -55.85},
            {-54, -54.84},
            {-53, -53.83},
            {-52, -52.83},
            {-51, -51.82},
            {-50, -50.81},
            {-49, -49.81},
            {-48, -48.8},
            {-47, -47.79},
            {-46, -46.79},
            {-45, -45.78},
            {-44, -44.77},
            {-43, -43.77},
            {-42, -42.76},
            {-41, -41.76},
            {-40, -40.75},
            {-39, -39.74},
            {-38, -38.74},
            {-37, -37.73},
            {-36, -36.73},
            {-35, -35.72},
            {-34, -34.72},
            {-33, -33.71},
            {-32, -32.7},
            {-31, -31.70},
            {-30, -30.69},
            {-29, -29.69},
            {-28, -28.68},
            {-27, -27.68},
            {-26, -26.67},
            {-25, -25.67},
            {-24, -24.66},
            {-23, -23.65},
            {-22, -22.65},
            {-21, -21.64},
            {-20, -20.64},
            {-19, -19.63},
            {-18, -18.63},
            {-17, -17.62},
            {-16, -16.62},
            {-15, -15.61},
            {-14, -14.61},
            {-13, -13.6},
            {-12, -12.6},
            {-11, -11.59},
            {-10, -10.59},
            {-9, -9.58},
            {-8, -8.58},
            {-7, -7.57},
            {-6, -6.57},
            {-5, -5.56},
            {-4, -4.56},
            {-3, -3.55},
            {-2, -2.55},
            {-1, -1.54},
            {0, -0.54},
            {1, 0.47},
            {2, 1.47},
            {3, 2.48},
            {4, 3.48},
            {5, 4.48},
            {6, 5.49},
            {7, 6.49},
            {8, 7.5},
            {9, 8.5},
            {10, 9.51},
            {11, 10.51},
            {12, 11.52},
            {13, 12.52},
            {14, 13.52},
            {15, 14.53},
            {16, 15.53},
            {17, 16.54},
            {18, 17.54},
            {19, 18.55},
            {20, 19.55},
            {21, 20.55},
            {22, 21.56},
            {23, 22.56},
            {24, 23.57},
            {25, 24.57},
            {26, 25.57},
            {27, 26.58},
            {28, 27.58},
            {29, 28.59},
            {30, 29.59},
            {31, 30.6},
            {32, 31.6},
            {33, 32.6},
            {34, 33.61},
            {35, 34.61},
            {36, 35.49},
            {37, 36.62},
            {38, 37.63},
            {39, 38.63},
            {40, 39.64},
            {41, 40.64},
            {42, 41.65},
            {43, 42.65},
            {44, 43.66},
            {45, 44.67},
            {46, 45.67},
            {47, 46.68},
            {48, 47.69},
            {49, 48.69},
            {50, 49.7},
            {51, 50.71},
            {52, 51.71},
            {53, 52.72},
            {54, 53.73},
            {55, 54.74},
            {56, 55.75},
            {57, 56.76},
            {58, 57.77},
            {59, 58.78},
            {60, 59.79},
            {61, 60.8},
            {62, 61.81},
            {63, 62.82},
            {64, 63.84},
            {65, 64.85},
            {66, 65.86},
            {67, 66.88},
            {68, 67.89},
            {69, 68.91},
            {70, 69.47},
            {71, 69.47},
            {72, 69.47},
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
