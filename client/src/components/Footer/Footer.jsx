import { NavLink } from 'react-router-dom'
import coffeeBeansDark from '../../resources/img/icons/main_beans_dark.svg'
import './footer.scss'

const Footer = () => {
	return (
		<footer>
			<div className='footer'>
				<div className='footer__wrapper'>
					<ul className='footer__list'>
						<NavLink to='/' className='footer__item'>Coffee house</NavLink>
						<NavLink to='/OurCoffee' className='footer__item'>Our coffee</NavLink>
						<NavLink to='/Pleasure' className='footer__item'>For your pleasure</NavLink>
					</ul>
				</div>
				<div className='divider-wrapper'>
					<div className='divider divider--first'></div>
					<img src={coffeeBeansDark} alt='coffee beans dark' />
					<div className='divider divider--second'></div>
				</div>
			</div>
		</footer>
	)
}

export default Footer