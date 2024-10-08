﻿import './aboutBeans.scss'
import BeansBlackImg from 'resources/img/icons/main_beans_dark.svg'

interface AboutBeansProps {
	title: string,
	backgroundImage: string
}

const AboutBeans = (props: AboutBeansProps) => {
	const { title, backgroundImage } = props
	
	return (
		<section className='beans'>
			<div className='beans__wrapper'>
				<img src={backgroundImage} alt='about beans' />
				<div className='beans__text'>
					<h2 className='beans__title'>{title}</h2>
					<div className='divider-wrapper'>
						<div className='divider divider--first'></div>
						<img src={BeansBlackImg} alt='beans img' />
						<div className='divider divider--second'></div>
					</div>
					<div className='beans__descr'>
						Extremity sweetness difficult behaviour he of. On disposal of as landlord horrible. <br /> <br /> Afraid at
						highly months
						do things on at. Situation recommend objection do intention <br /> so questions. <br /> As greatly removed
						calling pleased
						improve an. Last ask him cold feel <br /> met spot shy want. Children me laughing we prospect answered
						followed. At
						it went <br /> is song that held help face.
					</div>
				</div>
				<hr />
			</div>
		</section>
	)
}

export default AboutBeans