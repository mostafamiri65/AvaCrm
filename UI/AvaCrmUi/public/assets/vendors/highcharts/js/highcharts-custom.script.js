$(function () {
	"use strict";
	// chart 1
	Highcharts.chart('chart1', {
		chart: {
			plotBackgroundColor: null,
			plotBorderWidth: null,
			plotShadow: false,
			type: 'pie',
			styledMode: true
		},
		credits: {
			enabled: false
		},
		title: {
			text: 'Ų³Ł‡Ł… ŲØŲ§Ų²Ų§Ų± Ł…Ų±ŁŲ±ŚÆŲ± ŲÆŲ± Ų§Ų±ŲÆŪŲØŁ‡Ų´ŲŖ 1400'
		},
		tooltip: {
			useHTML: true,
			pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
		},
		accessibility: {
			point: {
				valueSuffix: '%'
			}
		},
		plotOptions: {
			useHTML: true,
			pie: {
				allowPointSelect: true,
				cursor: 'pointer',
				dataLabels: {
					useHTML: true,
					enabled: true,
					format: '<b>{point.name}</b>: {point.percentage:.1f} %'
				}
			}
		},
		series: [{
			name: 'ŲØŲ±Ł†ŲÆ Ł‡Ų§',
			colorByPoint: true,
			data: [{
				name: 'Ś©Ų±ŁŁ…',
				y: 61.41,
				sliced: true,
				selected: true
			}, {
				name: 'Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų±',
				y: 11.84
			}, {
				name: 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³',
				y: 10.85
			}, {
				name: 'Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬',
				y: 4.67
			}, {
				name: 'Ų³Ų§ŁŲ§Ų±Ū',
				y: 4.18
			}, {
				name: 'Ł…Ų±ŁŲ±ŚÆŲ± Ų³ŁŚÆŁ',
				y: 1.64
			}, {
				name: 'Ų§Ł¾Ų±Ų§',
				y: 1.6
			}, {
				name: 'Ł…Ų±ŁŲ±ŚÆŲ± QQ',
				y: 1.2
			}, {
				name: 'ŲÆŪŚÆŲ±',
				y: 2.61
			}]
		}]
	});
	// chart 2
	// Build the chart
	Highcharts.chart('chart2', {
		chart: {
			useHTML: true,
			plotBackgroundColor: null,
			plotBorderWidth: null,
			plotShadow: false,
			type: 'pie',
			styledMode: true
		},
		credits: {
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ų³Ł‡Ł… ŲØŲ§Ų²Ų§Ų± Ł…Ų±ŁŲ±ŚÆŲ± ŲÆŲ± Ų§Ų±ŲÆŪŲØŁ‡Ų´ŲŖ 1400'
		},
		legend: {
			rtl: true
		},
		tooltip: {
			useHTML: true,
			pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
		},
		accessibility: {
			useHTML: true,
			point: {
				useHTML: true,
				valueSuffix: '%'
			}
		},
		plotOptions: {
			useHTML: true,
			direction: 'rtl',
			pie: {
				useHTML: true,
				allowPointSelect: true,
				cursor: 'pointer',
				dataLabels: {
					useHTML: true,
					enabled: false
				},
				useHTML: true,
				showInLegend: true
			}
		},
		series: [{
			useHTML: true,
			name: 'ŲØŲ±Ł†ŲÆ Ł‡Ų§',
			colorByPoint: true,
			data: [{
				useHTML: true,
				name: 'Ś©Ų±ŁŁ…',
				y: 61.41,
				sliced: true,
				selected: true
			}, {
				name: 'Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų±',
				y: 11.84
			}, {
				name: 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³',
				y: 10.85
			}, {
				name: 'Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬',
				y: 4.67
			}, {
				name: 'Ų³Ų§ŁŲ§Ų±Ū',
				y: 4.18
			}, {
				name: 'ŲÆŪŚÆŲ±',
				y: 7.05
			}]
		}]
	});
	// chart 3
	Highcharts.chart('chart3', {
		chart: {
			useHTML: true,
			type: 'variablepie',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ł…Ł‚Ų§ŪŲ³Ł‡ Ś©Ų´ŁŲ±Ł‡Ų§ Ų§Ų² Ł†ŲøŲ± ŲŖŲ±Ų§Ś©Ł… Ų¬Ł…Ų¹ŪŲŖ Ł Ł…Ų³Ų§Ų­ŲŖ Ś©Ł„'
		},
		tooltip: {
			useHTML: true,
			headerFormat: '',
			pointFormat: '<span style="color:{point.color}">\u25CF</span> <b> {point.name}</b><br/>' + 'Ł…Ų³Ų§Ų­ŲŖ (Ś©ŪŁ„ŁŁ…ŲŖŲ± Ł…Ų±ŲØŲ¹) : <b>{point.y}</b><br/>' + 'ŲŖŲ±Ų§Ś©Ł… Ų¬Ł…Ų¹ŪŲŖ (Ł…Ų±ŲÆŁ… ŲÆŲ± Ł‡Ų± Ś©ŪŁ„ŁŁ…ŲŖŲ± Ł…Ų±ŲØŲ¹) : <b>{point.z}</b><br/>'
		},
		series: [{
			useHTML: true,
			minPointSize: 10,
			useHTML: true,
			innerSize: '20%',
			zMin: 0,
			name: 'Ś©Ų´ŁŲ±',
			data: [{
				name: 'Ų§Ų³Ł¾Ų§Ł†ŪŲ§',
				y: 505370,
				z: 92.9
			}, {
				name: 'ŁŲ±Ų§Ł†Ų³Ł‡',
				y: 551500,
				z: 118.7
			}, {
				name: 'Ł„Ł‡Ų³ŲŖŲ§Ł†',
				y: 312685,
				z: 124.6
			}, {
				name: 'Ų¬Ł…Ł‡ŁŲ±Ū Ś†Ś©',
				y: 78867,
				z: 137.5
			}, {
				name: 'Ų§ŪŲŖŲ§Ł„ŪŲ§',
				y: 301340,
				z: 201.8
			}, {
				name: 'Ų³ŁŪŪŲ³',
				y: 41277,
				z: 214.5
			}, {
				name: 'Ų¢Ł„Ł…Ų§Ł†',
				y: 357022,
				z: 235.6
			}]
		}]
	});
	// chart4
	// Make monochrome colors
	var pieColors = (function () {
		var colors = [],
			base = Highcharts.getOptions().colors[0],
			i;
		for (i = 0; i < 10; i += 1) {
			// Start out with a darkened base color (negative brighten), and end
			// up with a much brighter color
			colors.push(Highcharts.color(base).brighten((i - 3) / 7).get());
		}
		return colors;
	}());
	// Build the chart
	Highcharts.chart('chart4', {
		chart: {
			useHTML: true,
			plotBackgroundColor: null,
			plotBorderWidth: null,
			plotShadow: false,
			styledMode: true,
			type: 'pie'
		},
		credits: {
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ų³Ł‡Ł… ŲØŲ§Ų²Ų§Ų± Ł…Ų±ŁŲ±ŚÆŲ± ŲÆŲ± ŪŚ© ŁŲØŲ³Ų§ŪŲŖ Ų®Ų§ŲµŲ 1400'
		},
		tooltip: {
			useHTML: true,
			pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
		},
		accessibility: {
			point: {
				valueSuffix: '%'
			}
		},
		plotOptions: {
			useHTML: true,
			pie: {
				useHTML: true,
				allowPointSelect: true,
				cursor: 'pointer',
				colors: pieColors,
				dataLabels: {
					useHTML: true,
					enabled: true,
					format: '<b>{point.name}</b><br>{point.percentage:.1f} %',
					distance: -50,
					filter: {
						useHTML: true,
						property: 'percentage',
						operator: '>',
						value: 4
					}
				}
			}
		},
		series: [{
			useHTML: true,
			name: 'Ų³Ł‡Ł…',
			data: [{
				name: 'Ś©Ų±ŁŁ…',
				y: 61.41
			}, {
				name: 'Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų±',
				y: 11.84
			}, {
				name: 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³',
				y: 10.85
			}, {
				name: 'Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬',
				y: 4.67
			}, {
				name: 'Ų³Ų§ŁŲ§Ų±Ū',
				y: 4.18
			}, {
				name: 'ŲÆŪŚÆŲ±',
				y: 7.05
			}]
		}]
	});
	// chart 5
	var colors = Highcharts.getOptions().colors,
		categories = ['Ś©Ų±ŁŁ…', 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³', 'Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų±', 'Ų³Ų§ŁŲ§Ų±Ū', 'Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬', 'Ų§Ł¾Ų±Ų§', 'ŲÆŪŚÆŲ±'],
		data = [{
			useHTML: true,
			y: 62.74,
			color: colors[2],
			drilldown: {
				useHTML: true,
				name: 'Ś©Ų±ŁŁ…',
				categories: ['Ś©Ų±ŁŁ… v65.0', 'Ś©Ų±ŁŁ… v64.0', 'Ś©Ų±ŁŁ… v63.0', 'Ś©Ų±ŁŁ… v62.0', 'Ś©Ų±ŁŁ… v61.0', 'Ś©Ų±ŁŁ… v60.0', 'Ś©Ų±ŁŁ… v59.0', 'Ś©Ų±ŁŁ… v58.0', 'Ś©Ų±ŁŁ… v57.0', 'Ś©Ų±ŁŁ… v56.0', 'Ś©Ų±ŁŁ… v55.0', 'Ś©Ų±ŁŁ… v54.0', 'Ś©Ų±ŁŁ… v51.0', 'Ś©Ų±ŁŁ… v49.0', 'Ś©Ų±ŁŁ… v48.0', 'Ś©Ų±ŁŁ… v47.0', 'Ś©Ų±ŁŁ… v43.0', 'Ś©Ų±ŁŁ… v29.0'],
				data: [
					0.1,
					1.3,
					53.02,
					1.4,
					0.88,
					0.56,
					0.45,
					0.49,
					0.32,
					0.29,
					0.79,
					0.18,
					0.13,
					2.16,
					0.13,
					0.11,
					0.17,
					0.26
				]
			}
		}, {
			y: 10.57,
			color: colors[1],
			useHTML: true,
			drilldown: {
				useHTML: true,
				name: 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³',
				categories: ['ŁŲ§ŪŲ±ŁŲ§Ś©Ų³ v58.0', 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³ v57.0', 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³ v56.0', 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³ v55.0', 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³ v54.0', 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³ v52.0', 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³ v51.0', 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³ v50.0', 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³ v48.0', 'ŁŲ§ŪŲ±ŁŲ§Ś©Ų³ v47.0'],
				data: [
					1.02,
					7.36,
					0.35,
					0.11,
					0.1,
					0.95,
					0.15,
					0.1,
					0.31,
					0.12
				]
			}
		}, {
			y: 7.23,
			useHTML: true,
			color: colors[0],
			drilldown: {
				useHTML: true,
				name: 'Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų±',
				categories: ['Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų± v11.0', 'Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų± v10.0', 'Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų± v9.0', 'Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų± v8.0'],
				data: [
					6.2,
					0.29,
					0.27,
					0.47
				]
			}
		}, {
			y: 5.58,
			useHTML: true,
			color: colors[3],
			drilldown: {
				useHTML: true,
				name: 'Ų³Ų§ŁŲ§Ų±Ū',
				categories: ['Ų³Ų§ŁŲ§Ų±Ū v11.0', 'Ų³Ų§ŁŲ§Ų±Ū v10.1', 'Ų³Ų§ŁŲ§Ų±Ū v10.0', 'Ų³Ų§ŁŲ§Ų±Ū v9.1', 'Ų³Ų§ŁŲ§Ų±Ū v9.0', 'Ų³Ų§ŁŲ§Ų±Ū v5.1'],
				data: [
					3.39,
					0.96,
					0.36,
					0.54,
					0.13,
					0.2
				]
			}
		}, {
			y: 4.02,
			useHTML: true,
			color: colors[5],
			drilldown: {
				useHTML: true,
				name: 'Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬',
				categories: ['Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬ v16', 'Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬ v15', 'Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬ v14', 'Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬ v13'],
				data: [
					2.6,
					0.92,
					0.4,
					0.1
				]
			}
		}, {
			y: 1.92,
			useHTML: true,
			color: colors[4],
			drilldown: {
				useHTML: true,
				name: 'Ų§Ł¾Ų±Ų§',
				categories: ['Ų§Ł¾Ų±Ų§ v50.0', 'Ų§Ł¾Ų±Ų§ v49.0', 'Ų§Ł¾Ų±Ų§ v12.1'],
				data: [
					0.96,
					0.82,
					0.14
				]
			}
		}, {
			y: 7.62,
			useHTML: true,
			color: colors[6],
			drilldown: {
				useHTML: true,
				name: 'ŲÆŪŚÆŲ±',
				categories: ['ŲÆŪŚÆŲ±'],
				data: [
					7.62
				]
			}
		}],
		browserData = [],
		versionsData = [],
		i,
		j,
		dataLen = data.length,
		drillDataLen,
		brightness;
	// Build the data arrays
	for (i = 0; i < dataLen; i += 1) {
		// add browser data
		browserData.push({
			name: categories[i],
			y: data[i].y,
			color: data[i].color
		});
		// add version data
		drillDataLen = data[i].drilldown.data.length;
		for (j = 0; j < drillDataLen; j += 1) {
			brightness = 0.2 - (j / drillDataLen) / 5;
			versionsData.push({
				name: data[i].drilldown.categories[j],
				y: data[i].drilldown.data[j],
				color: Highcharts.color(data[i].color).brighten(brightness).get()
			});
		}
	}
	// Create the chart
	Highcharts.chart('chart5', {
		chart: {
			useHTML: true,
			type: 'pie',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ų³Ł‡Ł… ŲØŲ§Ų²Ų§Ų± Ł…Ų±ŁŲ±ŚÆŲ±Ų Ų§Ų±ŲÆŪŲØŁ‡Ų´ŲŖ 1400'
		},
		subtitle: {
			useHTML: true,
			text: 'Ł…Ł†ŲØŲ¹ : <a href="http://statcounter.com" target="_blank">statcounter.com</a>'
		},
		plotOptions: {
			useHTML: true,
			pie: {
				useHTML: true,
				shadow: false,
				center: ['50%', '50%']
			}
		},
		tooltip: {
			useHTML: true,
			valueSuffix: '%'
		},
		series: [{
			useHTML: true,
			name: 'Ł…Ų±ŁŲ±ŚÆŲ±',
			data: browserData,
			size: '60%',
			dataLabels: {
				formatter: function () {
					return this.y > 5 ? this.point.name : null;
				},
				color: '#ffffff',
				useHTML: true,
				distance: -30
			}
		}, {
			name: 'Ł†Ų³Ų®Ł‡',
			useHTML: true,
			data: versionsData,
			size: '80%',
			innerSize: '60%',
			dataLabels: {
				useHTML: true,
				formatter: function () {
					// display only if larger than 1
					return this.y > 1 ? '<b>' + this.point.name + ':</b> ' + this.y + '%' : null;
				}
			},
			id: 'versions'
		}],
		responsive: {
			rules: [{
				condition: {
					maxWidth: 400
				},
				chartOptions: {
					series: [{}, {
						id: 'versions',
						dataLabels: {
							useHTML: true,
							enabled: false
						}
					}]
				}
			}]
		}
	});
	// chart 6
	Highcharts.chart('chart6', {
		chart: {
			useHTML: true,
			plotBackgroundColor: null,
			plotBorderWidth: 0,
			styledMode: true,
			plotShadow: false
		},
		credits: {
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ł…Ų±ŁŲ±ŚÆŲ±<br>Ų§Ų´ŲŖŲ±Ų§Ś© ŚÆŲ°Ų§Ų±Ū<br>1400',
			align: 'center',
			verticalAlign: 'middle',
			y: 60
		},
		tooltip: {
			useHTML: true,
			pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
		},
		accessibility: {
			point: {
				useHTML: true,
				valueSuffix: '%'
			}
		},
		plotOptions: {
			useHTML: true,
			pie: {
				dataLabels: {
					useHTML: true,
					enabled: true,
					distance: -50,
					style: {
						useHTML: true,
						fontWeight: 'bold',
						color: 'white'
					}
				},
				startAngle: -90,
				useHTML: true,
				endAngle: 90,
				center: ['50%', '75%'],
				size: '110%'
			}
		},
		series: [{
			useHTML: true,
			type: 'pie',
			name: 'Ų³Ł‡Ł… Ł…Ų±ŁŲ±ŚÆŲ±',
			innerSize: '50%',
			data: [
				['Ś©Ų±ŁŁ…', 58.9],
				['ŁŲ§ŪŲ±ŁŲ§Ś©Ų³', 13.29],
				['Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų±', 13],
				['Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬', 3.78],
				['Ų³Ų§ŁŲ§Ų±Ū', 3.42], {
					name: 'ŲÆŪŚÆŲ±',
					y: 7.61,
					dataLabels: {
						useHTML: true,
						enabled: false
					}
				}
			]
		}]
	});
	// chart7
	Highcharts.chart('chart7', {
		chart: {
			type: 'bar',
			useHTML: true,
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ų¬Ł…Ų¹ŪŲŖ ŲŖŲ§Ų±ŪŲ®Ū Ų¬Ł‡Ų§Ł† ŲØŲ± Ų§Ų³Ų§Ų³ Ł…Ł†Ų§Ų·Ł‚'
		},
		subtitle: {
			useHTML: true,
			text: 'Ł…Ł†ŲØŲ¹ : <a href="https://en.wikipedia.org/wiki/World_population">Wikipedia.org</a>'
		},
		xAxis: {
			useHTML: true,
			categories: ['Ų¢ŁŲ±ŪŁ‚Ų§', 'Ų¢Ł…Ų±ŪŚ©Ų§', 'Ų¢Ų³ŪŲ§', 'Ų§Ų±ŁŁ¾Ų§', 'Ų§Ł‚ŪŲ§Ł†ŁŲ³ŪŁ‡'],
			title: {
				useHTML: true,
				text: null
			}
		},
		yAxis: {
			min: 0,
			title: {
				useHTML: true,
				text: 'Ų¬Ł…Ų¹ŪŲŖ (Ł…ŪŁ„ŪŁŁ†)',
				align: 'high'
			},
			labels: {
				useHTML: true,
				overflow: 'justify'
			}
		},
		tooltip: {
			useHTML: true,
			valueSuffix: ' Ł…ŪŁ„ŪŁŁ†'
		},
		plotOptions: {
			bar: {
				dataLabels: {
					enabled: true
				}
			}
		},
		legend: {
			useHTML: true,
			layout: 'vertical',
			align: 'right',
			rtl: true,
			verticalAlign: 'top',
			x: -40,
			y: 80,
			floating: true,
			borderWidth: 1,
			backgroundColor: Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
			shadow: true
		},
		credits: {
			enabled: false
		},
		series: [{
			name: 'Ų³Ų§Ł„ 1800',
			data: [107, 31, 635, 203, 2]
		}, {
			name: 'Ų³Ų§Ł„ 1900',
			data: [133, 156, 947, 408, 6]
		}, {
			name: 'Ų³Ų§Ł„ 2000',
			data: [814, 841, 3714, 727, 31]
		}, {
			name: 'Ų³Ų§Ł„ 2020',
			data: [1216, 1001, 4436, 738, 40]
		}]
	});
	// chart 8
	Highcharts.chart('chart8', {
		chart: {
			useHTML: true,
			type: 'column',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ł…ŪŲ§Ł†ŚÆŪŁ† ŲØŲ§Ų±Ł†ŲÆŚÆŪ Ł…Ų§Ł‡Ų§Ł†Ł‡'
		},
		subtitle: {
			useHTML: true,
			text: 'Ł…Ł†ŲØŲ¹ : WorldClimate.com'
		},
		legend: {
			rtl: true
		},
		xAxis: {
			useHTML: true,
			categories: ['ŁŲ±ŁŲ±ŲÆŪŁ†', 'Ų§Ų±ŲÆŪŲØŁ‡Ų´ŲŖ', 'Ų®Ų±ŲÆŲ§ŲÆ', 'ŲŖŪŲ±', 'Ł…Ų±ŲÆŲ§ŲÆ', 'Ų´Ł‡Ų±ŪŁŲ±', 'Ł…Ł‡Ų±', 'Ų¢ŲØŲ§Ł†', 'Ų¢Ų°Ų±', 'ŲÆŪ', 'ŲØŁ‡Ł…Ł†', 'Ų§Ų³ŁŁ†ŲÆ'],
			crosshair: true
		},
		yAxis: {
			useHTML: true,
			min: 0,
			title: {
				useHTML: true,
				text: 'ŲØŲ§Ų±Ł†ŲÆŚÆŪ (Ł…ŪŁ„Ū Ł…ŲŖŲ±)'
			}
		},
		tooltip: {
			useHTML: true,
			headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
			pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' + '<td style="padding:0"><b>{point.y:.1f} Ł…ŪŁ„Ū Ł…ŲŖŲ±</b></td></tr>',
			footerFormat: '</table>',
			shared: true,
			useHTML: true
		},
		plotOptions: {
			column: {
				pointPadding: 0.2,
				borderWidth: 0
			}
		},
		series: [{
			name: 'ŲŖŁ‡Ų±Ų§Ł†',
			data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4]
		}, {
			name: 'ŲŖŁŚ©ŪŁ',
			data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3]
		}, {
			name: 'Ł„Ł†ŲÆŁ†',
			data: [48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0, 59.6, 52.4, 65.2, 59.3, 51.2]
		}, {
			name: 'ŲØŲ±Ł„ŪŁ†',
			data: [42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8, 51.1]
		}]
	});
	// chart 9
	Highcharts.chart('chart9', {
		chart: {
			useHTML: true,
			type: 'bar',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ł†Ł…ŁŲÆŲ§Ų± Ų³ŲŖŁŁ†Ū Ų§Ł†ŲØŲ§Ų´ŲŖŁ‡'
		},
		tooltip: {
			useHTML: true
		},
		xAxis: {
			useHTML: true,
			categories: ['Ų³ŪŲØ', 'Ł¾Ų±ŲŖŁ‚Ų§Ł„', 'ŚÆŁ„Ų§ŲØŪ', 'ŚÆŲ±ŪŁ¾ ŁŲ±ŁŲŖ', 'Ł…ŁŲ²']
		},
		yAxis: {
			useHTML: true,
			min: 0,
			title: {
				useHTML: true,
				text: 'Ł…ŲµŲ±Ł Ś©Ł„ Ł…ŪŁŁ‡'
			}
		},
		legend: {
			useHTML: true,
			rtl: true,
			reversed: true
		},
		plotOptions: {
			useHTML: true,
			series: {
				useHTML: true,
				stacking: 'normal'
			}
		},
		series: [{
			name: 'Ų±Ų¶Ų§',
			data: [5, 3, 4, 7, 2]
		}, {
			name: 'Ł¾Ų±ŪŲ³Ų§',
			data: [2, 2, 3, 2, 1]
		}, {
			name: 'Ų³Ų§Ł†Ų§Ų²',
			data: [3, 4, 4, 2, 5]
		}]
	});
	// chart 10
	// Create the chart
	Highcharts.chart('chart10', {
		chart: {
			useHTML: true,
			type: 'column',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			useHTML: true,
			text: 'Ų³Ł‡Ł… ŲØŲ§Ų²Ų§Ų± Ł…Ų±ŁŲ±ŚÆŲ±Ų Ų®Ų±ŲÆŲ§ŲÆ 1400'
		},
		subtitle: {
			useHTML: true,
			useHTML: true,
			text: 'Ł…Ł†ŲØŲ¹ : <a href="http://statcounter.com" target="_blank">statcounter.com</a>'
		},
		accessibility: {
			announceNewData: {
				useHTML: true,
				enabled: true
			}
		},
		xAxis: {
			useHTML: true,
			type: 'category'
		},
		yAxis: {
			title: {
				useHTML: true,
				text: 'ŲÆŲ±ŲµŲÆ Ś©Ł„ Ų³Ł‡Ų§Ł… ŲØŲ§Ų²Ų§Ų±'
			}
		},
		legend: {
			useHTML: true,
			enabled: false
		},
		plotOptions: {
			useHTML: true,
			series: {
				useHTML: true,
				borderWidth: 0,
				dataLabels: {
					enabled: true,
					format: '{point.y:.1f}%'
				}
			}
		},
		tooltip: {
			useHTML: true,
			headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
			pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> Ų§Ų² Ł…Ų¬Ł…ŁŲ¹ Ś©Ł„<br/>'
		},
		series: [{
			name: "Ł…Ų±ŁŲ±ŚÆŲ±",
			colorByPoint: true,
			data: [{
				name: "Ś©Ų±ŁŁ…",
				y: 62.74,
				drilldown: "Ś©Ų±ŁŁ…"
			}, {
				name: "ŁŲ§ŪŲ±ŁŲ§Ś©Ų³",
				y: 10.57,
				drilldown: "ŁŲ§ŪŲ±ŁŲ§Ś©Ų³"
			}, {
				name: "Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų±",
				y: 7.23,
				drilldown: "Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų±"
			}, {
				name: "Ų³Ų§ŁŲ§Ų±Ū",
				y: 5.58,
				drilldown: "Ų³Ų§ŁŲ§Ų±Ū"
			}, {
				name: "Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬",
				y: 4.02,
				drilldown: "Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬"
			}, {
				name: "Ų§Ł¾Ų±Ų§",
				y: 1.92,
				drilldown: "Ų§Ł¾Ų±Ų§"
			}, {
				name: "ŲÆŪŚÆŲ±",
				y: 7.62,
				drilldown: null
			}]
		}],
		drilldown: {
			series: [{
				name: "Ś©Ų±ŁŁ…",
				id: "Ś©Ų±ŁŁ…",
				data: [
					["v65.0",
						0.1
					],
					["v64.0",
						1.3
					],
					["v63.0",
						53.02
					],
					["v62.0",
						1.4
					],
					["v61.0",
						0.88
					],
					["v60.0",
						0.56
					],
					["v59.0",
						0.45
					],
					["v58.0",
						0.49
					],
					["v57.0",
						0.32
					],
					["v56.0",
						0.29
					],
					["v55.0",
						0.79
					],
					["v54.0",
						0.18
					],
					["v51.0",
						0.13
					],
					["v49.0",
						2.16
					],
					["v48.0",
						0.13
					],
					["v47.0",
						0.11
					],
					["v43.0",
						0.17
					],
					["v29.0",
						0.26
					]
				]
			}, {
				name: "ŁŲ§ŪŲ±ŁŲ§Ś©Ų³",
				id: "ŁŲ§ŪŲ±ŁŲ§Ś©Ų³",
				data: [
					["v58.0",
						1.02
					],
					["v57.0",
						7.36
					],
					["v56.0",
						0.35
					],
					["v55.0",
						0.11
					],
					["v54.0",
						0.1
					],
					["v52.0",
						0.95
					],
					["v51.0",
						0.15
					],
					["v50.0",
						0.1
					],
					["v48.0",
						0.31
					],
					["v47.0",
						0.12
					]
				]
			}, {
				name: "Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų±",
				id: "Ų§ŪŁ†ŲŖŲ±Ł†ŲŖ Ų§Ś©Ų³Ł¾Ł„ŁŲ±Ų±",
				data: [
					["v11.0",
						6.2
					],
					["v10.0",
						0.29
					],
					["v9.0",
						0.27
					],
					["v8.0",
						0.47
					]
				]
			}, {
				name: "Ų³Ų§ŁŲ§Ų±Ū",
				id: "Ų³Ų§ŁŲ§Ų±Ū",
				data: [
					["v11.0",
						3.39
					],
					["v10.1",
						0.96
					],
					["v10.0",
						0.36
					],
					["v9.1",
						0.54
					],
					["v9.0",
						0.13
					],
					["v5.1",
						0.2
					]
				]
			}, {
				name: "Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬",
				id: "Ł…Ų§ŪŚ©Ų±ŁŲ³Ų§ŁŲŖ Ų§Ų¬",
				data: [
					["v16",
						2.6
					],
					["v15",
						0.92
					],
					["v14",
						0.4
					],
					["v13",
						0.1
					]
				]
			}, {
				name: "Ų§Ł¾Ų±Ų§",
				id: "Ų§Ł¾Ų±Ų§",
				data: [
					["v50.0",
						0.96
					],
					["v49.0",
						0.82
					],
					["v12.1",
						0.14
					]
				]
			}]
		}
	});
	// chart 11
	Highcharts.chart('chart11', {
		chart: {
			useHTML: true,
			type: 'area',
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		accessibility: {
			useHTML: true,
			description: 'ŲŖŁŲ¶ŪŲ­Ų§ŲŖ ŲŖŲµŁŪŲ±: ŪŚ© Ł†Ł…ŁŲÆŲ§Ų± Ł…Ł†Ų·Ł‚Ł‡ Ų°Ų®Ų§ŪŲ± Ł‡Ų³ŲŖŁ‡ Ų§Ū Ų§ŪŲ§Ł„Ų§ŲŖ Ł…ŲŖŲ­ŲÆŁ‡ Ų¢Ł…Ų±ŪŚ©Ų§ Ł Ų§ŲŖŲ­Ų§ŲÆ Ų¬Ł…Ų§Ł‡ŪŲ± Ų´ŁŲ±ŁŪ Ų³ŁŲ³ŪŲ§Ł„ŪŲ³ŲŖŪ Ų±ŁŲ³ŪŁ‡ / Ų±ŁŲ³ŪŁ‡ Ų±Ų§ ŲØŪŁ† Ų³Ų§Ł„Ł‡Ų§Ū 1945 Ł 2017 Ł…Ł‚Ų§ŪŲ³Ł‡ Ł…Ū Ś©Ł†ŲÆ. ŲŖŲ¹ŲÆŲ§ŲÆ Ų³Ł„Ų§Ų­ Ł‡Ų§Ū Ł‡Ų³ŲŖŁ‡ Ų§Ū ŲÆŲ± Ł…Ų­ŁŲ± Y Ł Ų³Ų§Ł„Ł‡Ų§ ŲÆŲ± Ł…Ų­ŁŲ± X Ų±Ų³Ł… Ų´ŲÆŁ‡ Ų§Ų³ŲŖ. Ł†Ł…ŁŲÆŲ§Ų± ŲŖŲ¹Ų§Ł…Ł„Ū Ų§Ų³ŲŖ Ł Ł…Ū ŲŖŁŲ§Ł† Ų³Ų·Ų­ Ų°Ų®ŪŲ±Ł‡ Ų³Ų§Ł„ ŲØŁ‡ Ų³Ų§Ł„ Ų±Ų§ ŲØŲ±Ų§Ū Ł‡Ų± Ś©Ų´ŁŲ± Ų±ŲÆŪŲ§ŲØŪ Ś©Ų±ŲÆ. Ų§ŪŲ§Ł„Ų§ŲŖ Ł…ŲŖŲ­ŲÆŁ‡ 6 Ų³Ł„Ų§Ų­ Ł‡Ų³ŲŖŁ‡ Ų§Ū ŲÆŲ± Ų§ŁŲ§ŪŁ„ Ų¹ŲµŲ± Ł‡Ų³ŲŖŁ‡ Ų§Ū ŲÆŲ± Ų³Ų§Ł„ 1945 Ų°Ų®ŪŲ±Ł‡ Ś©Ų±ŲÆŁ‡ Ų§Ų³ŲŖ. Ų§ŪŁ† ŲŖŲ¹ŲÆŲ§ŲÆ ŲØŲŖŲÆŲ±ŪŲ¬ ŲŖŲ§ Ų³Ų§Ł„ 1950 Ś©Ł‡ Ų§ŲŖŲ­Ų§ŲÆ Ų¬Ł…Ų§Ł‡ŪŲ± Ų´ŁŲ±ŁŪ ŲØŲ§ 6 Ų³Ł„Ų§Ų­ ŁŲ§Ų±ŲÆ Ł…Ų³Ų§ŲØŁ‚Ł‡ ŲŖŲ³Ł„ŪŲ­Ų§ŲŖŪ Ł…Ū Ų´ŁŲÆ Ų ŲØŁ‡ 369 Ų³Ł„Ų§Ų­ Ł‡Ų³ŲŖŁ‡ Ų§Ū Ų§ŁŲ²Ų§ŪŲ´ Ł…Ū ŪŲ§ŲØŲÆ. ŲÆŲ± Ų§ŪŁ† Ł…Ų±Ų­Ł„Ł‡ Ų Ų§ŪŲ§Ł„Ų§ŲŖ Ł…ŲŖŲ­ŲÆŁ‡ Ų´Ų±ŁŲ¹ ŲØŁ‡ Ų³Ų§Ų®ŲŖ Ų³Ų±ŪŲ¹ Ų°Ų®Ų§ŪŲ± Ų®ŁŲÆ Ł…Ū Ś©Ł†ŲÆ Ś©Ł‡ ŲÆŲ± Ų³Ų§Ł„ 1966 ŲØŲ§ 32Ų040 Ś©Ł„Ų§Ł‡Ś© ŲÆŲ± Ł…Ł‚Ų§ŪŲ³Ł‡ ŲØŲ§ 7Ų089 Ś©Ł„Ų§Ł‡ŲØŲ±ŲÆŲ§Ų±Ū Ų§ŲŖŲ­Ų§ŲÆ Ų¬Ł…Ų§Ł‡ŪŲ± Ų´ŁŲ±ŁŪ (Ų´ŁŲ±ŁŪ) ŲØŁ‡ Ł¾Ų§ŪŲ§Ł† Ų±Ų³ŪŲÆ. Ų§Ų² Ų§ŪŁ† Ų§ŁŲ¬ ŲÆŲ± Ų³Ų§Ł„ 1966 Ų ŲØŲ§ ŚÆŲ³ŲŖŲ±Ų´ Ų°Ų®Ų§ŪŲ± Ų§ŲŖŲ­Ų§ŲÆ Ų¬Ł…Ų§Ł‡ŪŲ± Ų´ŁŲ±ŁŪ (Ų´ŁŲ±ŁŪ) Ų Ų°Ų®Ų§ŪŲ± Ų§ŪŲ§Ł„Ų§ŲŖ Ł…ŲŖŲ­ŲÆŁ‡ ŲØŁ‡ ŲŖŲÆŲ±ŪŲ¬ Ś©Ų§Ł‡Ų´ Ł…Ū ŪŲ§ŲØŲÆ. ŲŖŲ§ Ų³Ų§Ł„ 1978 Ų§ŲŖŲ­Ų§ŲÆ Ų¬Ł…Ų§Ł‡ŪŲ± Ų´ŁŲ±ŁŪ Ų³ŁŲ³ŪŲ§Ł„ŪŲ³ŲŖŪ Ų´Ś©Ų§Ł Ł‡Ų³ŲŖŁ‡ Ų§Ū Ų±Ų§ ŲÆŲ± 25393 Ł…ŁŲ±ŲÆ Ś©Ų§Ł‡Ų´ ŲÆŲ§ŲÆ. Ų°Ų®Ų§ŪŲ± Ų§ŲŖŲ­Ų§ŲÆ Ų¬Ł…Ų§Ł‡ŪŲ± Ų´ŁŲ±ŁŪ Ų³Ų§ŲØŁ‚ ŲØŁ‡ Ų±Ų´ŲÆ Ų®ŁŲÆ Ų§ŲÆŲ§Ł…Ł‡ Ł…Ū ŲÆŁ‡ŲÆ ŲŖŲ§ Ų¬Ų§ŪŪ Ś©Ł‡ ŲÆŲ± Ų³Ų§Ł„ 1986 ŲÆŲ± Ł…Ł‚Ų§ŪŲ³Ł‡ ŲØŲ§ 24401 Ų²Ų±Ų§ŲÆŲ®Ų§Ł†Ł‡ Ų§ŪŲ§Ł„Ų§ŲŖ Ł…ŲŖŲ­ŲÆŁ‡ ŲØŁ‡ Ų§ŁŲ¬ 45000 Ų±Ų³ŪŲÆ. Ų§Ų² Ų³Ų§Ł„ 1986 Ų Ų°Ų®Ų§ŪŲ± Ł‡Ų³ŲŖŁ‡ Ų§Ū Ł‡Ų± ŲÆŁ Ś©Ų´ŁŲ± Ų´Ų±ŁŲ¹ ŲØŁ‡ Ų³Ł‚ŁŲ· Ł…Ū Ś©Ł†ŲÆ. ŲŖŲ§ Ų³Ų§Ł„ 2000 Ų Ų§ŪŁ† ŲŖŲ¹ŲÆŲ§ŲÆ ŲØŲ±Ų§Ū Ų§ŪŲ§Ł„Ų§ŲŖ Ł…ŲŖŲ­ŲÆŁ‡ Ł Ų±ŁŲ³ŪŁ‡ ŲØŁ‡ ŲŖŲ±ŲŖŪŲØ ŲØŁ‡ 10Ų577 Ł 21Ų000 Ų±Ų³ŪŲÆŁ‡ Ų§Ų³ŲŖ. Ų§ŪŁ† Ś©Ų§Ł‡Ų´ Ł‡Ų§ ŲŖŲ§ Ų³Ų§Ł„ 2017 Ų§ŲÆŲ§Ł…Ł‡ ŲÆŲ§Ų±ŲÆ Ł ŲÆŲ± Ų¢Ł† Ų²Ł…Ų§Ł† Ų§ŪŲ§Ł„Ų§ŲŖ Ł…ŲŖŲ­ŲÆŁ‡ 4Ų018 Ų³Ł„Ų§Ų­ ŲÆŲ± Ł…Ł‚Ų§ŪŲ³Ł‡ ŲØŲ§ 4500 Ų³Ł„Ų§Ų­ Ų±ŁŲ³ŪŁ‡ ŲÆŲ± Ų§Ų®ŲŖŪŲ§Ų± ŲÆŲ§Ų±ŲÆ.'
		},
		title: {
			useHTML: true,
			text: 'Ų°Ų®Ų§ŪŲ± Ł‡Ų³ŲŖŁ‡ Ų§Ū Ų§ŪŲ§Ł„Ų§ŲŖ Ł…ŲŖŲ­ŲÆŁ‡ Ł Ų§ŲŖŲ­Ų§ŲÆ Ų¬Ł…Ų§Ł‡ŪŲÆ Ų´ŁŲ±ŁŪ Ų³ŁŲ³ŪŲ§Ł„ŪŲ³ŲŖŪ'
		},
		subtitle: {
			useHTML: true,
			text: 'Ł…Ł†ŲØŲ¹ : <a href="https://thebulletin.org/2006/july/global-nuclear-stockpiles-1945-2006">' + 'thebulletin.org</a> Ł <a href="https://www.armscontrol.org/factsheets/Nuclearweaponswhohaswhat">' + 'armscontrol.org</a>'
		},
		legend: {
			rtl: true
		},
		xAxis: {
			useHTML: true,
			allowDecimals: false,
			labels: {
				useHTML: true,
				formatter: function () {
					return this.value; // clean, unformatted number for year
				}
			},
			accessibility: {
				useHTML: true,
				rangeDescription: 'Ł…Ų­ŲÆŁŲÆŁ‡ : 1940 ŲŖŲ§ 2017'
			}
		},
		yAxis: {
			title: {
				useHTML: true,
				text: 'Ś©Ų´ŁŲ± Ł‡Ų§Ū Ų³Ł„Ų§Ų­ Ł‡Ų³ŲŖŁ‡ Ų§Ū'
			},
			labels: {
				useHTML: true,
				formatter: function () {
					return this.value / 1000 + 'k';
				}
			}
		},
		tooltip: {
			useHTML: true,
			pointFormat: '{series.name} Ų°Ų®ŪŲ±Ł‡ Ś©Ų±ŲÆŁ‡ ŲØŁŲÆ <b>{point.y:,.0f}</b><br/>Ś©Ł„Ų§Ł‡Ś© ŲÆŲ± Ų³Ų§Ł„ {point.x}'
		},
		plotOptions: {
			useHTML: true,
			area: {
				useHTML: true,
				pointStart: 1940,
				marker: {
					useHTML: true,
					enabled: false,
					symbol: 'circle',
					radius: 2,
					states: {
						hover: {
							useHTML: true,
							enabled: true
						}
					}
				}
			}
		},
		series: [{
			name: 'Ų§ŪŲ§Ł„Ų§ŲŖ Ł…ŲŖŲ­ŲÆŁ‡ Ų¢Ł…Ų±ŪŚ©Ų§',
			data: [
				null, null, null, null, null, 6, 11, 32, 110, 235,
				369, 640, 1005, 1436, 2063, 3057, 4618, 6444, 9822, 15468,
				20434, 24126, 27387, 29459, 31056, 31982, 32040, 31233, 29224, 27342,
				26662, 26956, 27912, 28999, 28965, 27826, 25579, 25722, 24826, 24605,
				24304, 23464, 23708, 24099, 24357, 24237, 24401, 24344, 23586, 22380,
				21004, 17287, 14747, 13076, 12555, 12144, 11009, 10950, 10871, 10824,
				10577, 10527, 10475, 10421, 10358, 10295, 10104, 9914, 9620, 9326,
				5113, 5113, 4954, 4804, 4761, 4717, 4368, 4018
			]
		}, {
			name: 'Ų§ŲŖŲ®Ų§ŲÆ Ų¬Ł…Ų§Ł‡ŪŲ± Ų´ŁŲ±ŁŪ Ų³ŁŲ³ŪŲ§Ł„ŪŲ³ŲŖŪ/Ų±ŁŲ³ŪŁ‡',
			data: [null, null, null, null, null, null, null, null, null, null,
				5, 25, 50, 120, 150, 200, 426, 660, 869, 1060,
				1605, 2471, 3322, 4238, 5221, 6129, 7089, 8339, 9399, 10538,
				11643, 13092, 14478, 15915, 17385, 19055, 21205, 23044, 25393, 27935,
				30062, 32049, 33952, 35804, 37431, 39197, 45000, 43000, 41000, 39000,
				37000, 35000, 33000, 31000, 29000, 27000, 25000, 24000, 23000, 22000,
				21000, 20000, 19000, 18000, 18000, 17000, 16000, 15537, 14162, 12787,
				12600, 11400, 5500, 4512, 4502, 4502, 4500, 4500
			]
		}]
	});
	// chart 12
	Highcharts.chart('chart12', {
		chart: {
			useHTML: true,
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ł†Ł…ŁŲÆŲ§Ų± ŲŖŲ±Ś©ŪŲØŪ'
		},
		tooltip: {
			useHTML: true
		},
		legend: {
			rtl: true
		},
		xAxis: {
			useHTML: true,
			categories: ['Ų³ŪŲØ', 'Ł¾Ų±ŲŖŁ‚Ų§Ł„', 'ŚÆŁ„Ų§ŲØŪ', 'Ł…ŁŲ²', 'Ų¢Ł„Ł']
		},
		labels: {
			items: [{
				useHTML: true,
				html: 'Ł…ŲµŲ±Ł Ś©Ł„ Ł…ŪŁŁ‡',
				style: {
					useHTML: true,
					left: '83px',
					top: '18px',
					color: ( // theme
						Highcharts.defaultOptions.title.style && Highcharts.defaultOptions.title.style.color) || 'black'
				}
			}]
		},
		series: [{
			type: 'column',
			name: 'Ų±Ų¶Ų§',
			data: [3, 2, 1, 3, 4]
		}, {
			type: 'column',
			name: 'Ł¾Ų±ŪŲ³Ų§',
			data: [2, 3, 5, 7, 6]
		}, {
			type: 'column',
			name: 'Ų³Ų§Ł†Ų§Ų²',
			data: [4, 3, 3, 9, 0]
		}, {
			type: 'spline',
			name: 'Ł…ŪŲ§Ł†ŚÆŪŁ†',
			data: [3, 2.67, 3, 6.33, 3.33],
			marker: {
				lineWidth: 2,
				lineColor: Highcharts.getOptions().colors[3],
				fillColor: 'white'
			}
		}, {
			type: 'pie',
			useHTML: true,
			name: 'Ł…ŲµŲ±Ł Ś©Ł„',
			data: [{
				name: 'Ų±Ų¶Ų§',
				y: 13,
				color: Highcharts.getOptions().colors[0] // Jane's color
			}, {
				name: 'Ł¾Ų±ŪŲ³Ų§',
				y: 23,
				color: Highcharts.getOptions().colors[1] // John's color
			}, {
				name: 'Ų³Ų§Ł†Ų§Ų²',
				y: 19,
				color: Highcharts.getOptions().colors[2] // Joe's color
			}],
			center: [100, 80],
			size: 100,
			useHTML: true,
			showInLegend: false,
			dataLabels: {
				useHTML: true,
				enabled: false
			}
		}]
	});
	// chart 13
	Highcharts.chart('chart13', {
		chart: {
			zoomType: 'xy',
			styledMode: true
		},
		credits: {
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ł…ŪŲ§Ł†ŚÆŪŁ† ŲÆŁ…Ų§Ū Ł…Ų§Ł‡Ų§Ł†Ł‡ Ł ŲØŲ§Ų±Ł†ŲÆŚÆŪ ŲÆŲ± ŲŖŁŚ©ŪŁ'
		},
		subtitle: {
			useHTML: true,
			text: 'Ł…Ł†ŲØŲ¹ : WorldClimate.com'
		},
		xAxis: [{
			categories: ['ŁŲ±ŁŲ±ŲÆŪŁ†', 'Ų§Ų±ŲÆŪŲØŁ‡Ų´ŲŖ', 'Ų®Ų±ŲÆŲ§ŲÆ', 'ŲŖŪŲ±', 'Ł…Ų±ŲÆŲ§ŲÆ', 'Ų´Ł‡Ų±ŪŁŲ±', 'Ł…Ł‡Ų±', 'Ų¢ŲØŲ§Ł†', 'Ų¢Ų°Ų±', 'ŲÆŪ', 'ŲØŁ‡Ł…Ł†', 'Ų§Ų³ŁŁ†ŲÆ'],
			crosshair: true
		}],
		yAxis: [{ // Primary yAxis
			labels: {
				format: '{value}Ā°C',
				style: {
					color: Highcharts.getOptions().colors[1]
				}
			},
			title: {
				text: 'ŲÆŁ…Ų§',
				style: {
					color: Highcharts.getOptions().colors[1]
				}
			}
		}, { // Secondary yAxis
			title: {
				text: 'ŲØŲ§Ų±Ł†ŲÆŚÆŪ',
				style: {
					color: Highcharts.getOptions().colors[0]
				}
			},
			labels: {
				format: '{value} mm',
				style: {
					color: Highcharts.getOptions().colors[0]
				}
			},
			opposite: true
		}],
		tooltip: {
			useHTML: true,
			shared: true
		},
		legend: {
			useHTML: true,
			rtl: true,
			layout: 'vertical',
			align: 'left',
			x: 120,
			verticalAlign: 'top',
			y: 100,
			floating: true,
			backgroundColor: Highcharts.defaultOptions.legend.backgroundColor || // theme
				'rgba(255,255,255,0.25)'
		},
		series: [{
			name: 'ŲØŲ§Ų±Ł†ŲÆŚÆŪ',
			type: 'column',
			yAxis: 1,
			data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4],
			tooltip: {
				valueSuffix: ' Ł…ŪŁ„Ū Ł…ŲŖŲ±'
			}
		}, {
			name: 'ŲÆŁ…Ų§',
			type: 'spline',
			data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6],
			tooltip: {
				valueSuffix: 'Ā°C'
			}
		}]
	});
	// chart 14
	Highcharts.chart('chart14', {
		chart: {
			useHTML: true,
			type: 'column',
			styledMode: true
		},
		title: {
			useHTML: true,
			text: 'Ł†Ł…ŁŲÆŲ§Ų± Ų³ŲŖŁŁ†Ū ŲØŲ§ Ł…Ł‚Ų§ŲÆŪŲ± Ł…Ł†ŁŪ'
		},
		legend: {
			rtl: true
		},
		tooltip: {
			useHTML: true
		},
		xAxis: {
			useHTML: true,
			categories: ['Ų³ŪŲØ', 'Ł¾Ų±ŲŖŁ‚Ų§Ł„', 'ŚÆŁ„Ų§ŲØŪ', 'ŚÆŲ±ŪŁ¾ ŁŲ±ŁŲŖ', 'Ł…ŁŲ²']
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		series: [{
			name: 'Ų±Ų¶Ų§',
			data: [5, 3, 4, 7, 2]
		}, {
			name: 'Ł¾Ų±ŪŲ³Ų§',
			data: [2, -2, -3, 2, 1]
		}, {
			name: 'Ų³Ų§Ł†Ų§Ų²',
			data: [3, 4, 4, -2, 5]
		}]
	});
	// chart 15
	Highcharts.chart('chart15', {
		chart: {
			type: 'column',
			useHTML: true,
			styledMode: true
		},
		credits: {
			useHTML: true,
			enabled: false
		},
		title: {
			useHTML: true,
			text: 'Ł†Ł…ŁŲÆŲ§Ų± Ų³ŲŖŁŁ†Ū Ų§Ł†ŲØŲ§Ų´ŲŖŁ‡ Ų´ŲÆŁ‡'
		},
		xAxis: {
			useHTML: true,
			categories: ['Ų³ŪŲØ', 'Ł¾Ų±ŲŖŁ‚Ų§Ł„', 'ŚÆŁ„Ų§ŲØŪ', 'ŚÆŲ±ŪŁ¾ ŁŲ±ŁŲŖ', 'Ł…ŁŲ²']
		},
		yAxis: {
			useHTML: true,
			min: 0,
			title: {
				useHTML: true,
				text: 'Ł…ŲµŲ±Ł Ś©Ł„ Ł…ŪŁŁ‡'
			},
			stackLabels: {
				enabled: true,
				style: {
					useHTML: true,
					fontWeight: 'bold',
					color: ( // theme
						Highcharts.defaultOptions.title.style && Highcharts.defaultOptions.title.style.color) || 'gray'
				}
			}
		},
		legend: {
			useHTML: true,
			align: 'right',
			rtl: true,
			x: -30,
			verticalAlign: 'top',
			y: 25,
			floating: true,
			backgroundColor: Highcharts.defaultOptions.legend.backgroundColor || 'white',
			borderColor: '#CCC',
			borderWidth: 1,
			shadow: false
		},
		tooltip: {
			useHTML: true,
			headerFormat: '<b>{point.x}</b><br/>',
			pointFormat: '{series.name}: {point.y}<br/>Ł…Ų¬Ł…ŁŲ¹ : {point.stackTotal}'
		},
		plotOptions: {
			column: {
				stacking: 'normal',
				dataLabels: {
					enabled: true
				}
			}
		},
		series: [{
			name: 'Ų±Ų¶Ų§',
			data: [5, 3, 4, 7, 2]
		}, {
			name: 'Ł¾Ų±ŪŲ³Ų§',
			data: [2, 2, 3, 2, 1]
		}, {
			name: 'Ų³Ų§Ł†Ų§Ų²',
			data: [3, 4, 4, 2, 5]
		}]
	});
});