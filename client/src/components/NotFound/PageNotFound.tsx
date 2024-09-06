import { Link } from 'react-router-dom'
import './pageNotFound.scss'

const PageNotFound = () => {
	return (
		<div className='notFound'>
			<h2 className='notFound__title'>404 - Page Not Found</h2>
			<div className='notFound__descr'>Sorry, the page you are looking for does not exist.</div>
			<p className='notFound__suggestion'>You might want to check the URL or return to the homepage.</p>
			<Link to='/' className='notFound__link'>Go to Homepage</Link>
		</div>
	)
}

export default PageNotFound
