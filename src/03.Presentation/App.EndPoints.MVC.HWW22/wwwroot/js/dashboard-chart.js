document.addEventListener("DOMContentLoaded", function () {
    // 1. دریافت داده‌ها از تگ HTML
    // ما دیتا را به صورت JSON در اتریبیوت data-chart-data قرار دادیم
    const chartDataElement = document.getElementById('salesChartData');

    if (chartDataElement) {
        // پارس کردن جیسون به آبجکت جاوااسکریپت
        const rawData = JSON.parse(chartDataElement.getAttribute('data-json'));

        // 2. جدا کردن تاریخ‌ها (محور X) و تعداد (محور Y)
        // تبدیل تاریخ میلادی به شمسی برای نمایش زیباتر
        const labels = rawData.map(item => new Date(item.date).toLocaleDateString('fa-IR'));
        const dataPoints = rawData.map(item => item.count);

        // 3. رسم نمودار
        const ctx = document.getElementById('salesChart').getContext('2d');
        new Chart(ctx, {
            type: 'line', // نوع نمودار: خطی
            data: {
                labels: labels,
                datasets: [{
                    label: 'تعداد سفارشات روزانه',
                    data: dataPoints,
                    borderColor: '#4e73df',
                    backgroundColor: 'rgba(78, 115, 223, 0.1)',
                    pointBackgroundColor: '#4e73df',
                    pointBorderColor: '#fff',
                    pointHoverBackgroundColor: '#fff',
                    pointHoverBorderColor: '#4e73df',
                    fill: true,
                    tension: 0.3 // برای نرم شدن خطوط نمودار
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        labels: {
                            font: {
                                family: 'Vazir' // تنظیم فونت نمودار
                            }
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            stepSize: 1 // چون تعداد سفارش اعشاری نمی‌شود
                        }
                    }
                }
            }
        });
    }
});