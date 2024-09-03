import coffeeBeansDark from '../../resources/img/icons/main_beans_dark.svg'
import './about.scss'

const About = () => {
	return (
		<section className='about'>
			<div className='about__wrapper'>
				<h2 className='about__header'>About Us</h2>
				<div className='divider-wrapper'>
					<div className='divider divider--first'></div>
					<img src={coffeeBeansDark} alt='coffee beans dark' />
					<div className='divider divider--second'></div>
				</div>
				<div className='about__description-wrapper'>
					<div className='about__description--first'>
						Extremity sweetness difficult behaviour he of. On disposal of as landlord
						horrible. Afraid at highly months do things on at. Situation recommend objection do intention
						so questions. As greatly removed calling pleased improve an. Last ask him cold feel
						met spot shy want. Children me laughing we prospect answered followed. At it went
						is song that held help face.
					</div>
					<div className='about__description--second'>
						Now residence dashwoods she excellent you. Shade being under his bed her, Much
						read on as draw. Blessing for ignorant exercise any yourself unpacked. Pleasant
						horrible but confined day end marriage. Eagerness furniture set preserved far
						recommend. Did even but nor are most gave hope. Secure active living depend son
						repair day ladies now.
					</div>
				</div>
			</div>
		</section>
	)
}

export default About
